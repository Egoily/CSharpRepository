using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using ee.Core.Net.Extensibility;
using ee.Core.Net.Retry;
using Newtonsoft.Json.Linq;


namespace ee.Core.Net.Internal
{
    /// <summary>Builds and dispatches an asynchronous HTTP request, and asynchronously parses the response.</summary>
    public class Request : IRequest
    {

        /// <summary>Dispatcher that executes the request.</summary>
        private readonly Func<IRequest, Task<HttpResponseMessage>> Dispatcher;


        /// <summary>The underlying HTTP request message.</summary>
        public HttpRequestMessage Message { get; }

        /// <summary>The formatters used for serializing and deserializing message bodies.</summary>
        public MediaTypeFormatterCollection Formatters { get; }

        /// <summary>Middle-ware classes which can intercept and modify HTTP requests and responses.</summary>
        public ICollection<IHttpFilter> Filters { get; }

        /// <summary>The optional token used to cancel async operations.</summary>
        public CancellationToken CancellationToken { get; private set; }

        /// <summary>The request coordinator.</summary>
        public IRequestCoordinator RequestCoordinator { get; private set; }

        /// <summary>The request options.</summary>
        public RequestOptions Options { get; }

        public IEnumerable<KeyValuePair<string, object>> Arguments { get; }


        /// <summary>Construct an instance.</summary>
        /// <param name="message">The underlying HTTP request message.</param>
        /// <param name="formatters">The formatters used for serializing and deserializing message bodies.</param>
        /// <param name="dispatcher">Executes an HTTP request.</param>
        /// <param name="filters">Middle-ware classes which can intercept and modify HTTP requests and responses.</param>
        public Request(HttpRequestMessage message, MediaTypeFormatterCollection formatters, Func<IRequest, Task<HttpResponseMessage>> dispatcher, ICollection<IHttpFilter> filters)
        {
            this.Message = message;
            this.Formatters = formatters;
            this.Dispatcher = dispatcher;
            this.Filters = filters;
            this.CancellationToken = CancellationToken.None;
            this.RequestCoordinator = null;
            this.Options = new RequestOptions();
            Arguments = new Dictionary<string, object>();

        }


        /// <summary>Set the body content of the HTTP request.</summary>
        /// <param name="bodyBuilder">The HTTP body builder.</param>
        /// <returns>Returns the request builder for chaining.</returns>
        public virtual IRequest WithBody(Func<IBodyBuilder, HttpContent> bodyBuilder)
        {
            this.Message.Content = bodyBuilder(new BodyBuilder(this));
            return this;
        }

        /// <summary>Set an HTTP header.</summary>
        /// <param name="key">The key of the HTTP header.</param>
        /// <param name="value">The value of the HTTP header.</param>
        /// <returns>Returns the request builder for chaining.</returns>
        public virtual IRequest WithHeader(string key, string value)
        {
            this.Message.Headers.Add(key, value);
            return this;
        }

        /// <summary>Add an authentication header.</summary>
        /// <param name="scheme">The scheme to use for authorization. e.g.: "Basic", "Bearer".</param>
        /// <param name="parameter">The credentials containing the authentication information.</param>
        public virtual IRequest WithAuthentication(string scheme, string parameter)
        {
            this.Message.Headers.Authorization = new AuthenticationHeaderValue(scheme, parameter);
            return this;
        }

        /// <summary>Add an HTTP query string argument.</summary>
        /// <param name="key">The key of the query argument.</param>
        /// <param name="value">The value of the query argument.</param>
        /// <returns>Returns the request builder for chaining.</returns>
        public virtual IRequest WithArgument(string key, object value)
        {
            this.Message.RequestUri = this.Message.RequestUri.WithArguments(this.Options.IgnoreNullArguments ?? false, new KeyValuePair<string, object>(key, value));
            return this;
        }

        /// <summary>Add HTTP query string arguments.</summary>
        /// <param name="arguments">The arguments to add.</param>
        /// <returns>Returns the request builder for chaining.</returns>
        /// <example><code>client.WithArguments(new[] { new KeyValuePair&lt;string, string&gt;("genre", "drama"), new KeyValuePair&lt;string, int&gt;("genre", "comedy") })</code></example>
        public virtual IRequest WithArguments<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> arguments)
        {
            if (arguments == null)
                return this;

            KeyValuePair<string, object>[] args = (
                from arg in arguments
                let key = arg.Key?.ToString()
                where !string.IsNullOrWhiteSpace(key)
                select new KeyValuePair<string, object>(key, arg.Value)
            ).ToArray();
            this.Message.RequestUri = this.Message.RequestUri.WithArguments(this.Options.IgnoreNullArguments ?? false, args);
            return this;
        }

        /// <summary>Add HTTP query string arguments.</summary>
        /// <param name="arguments">An anonymous object where the property names and values are used.</param>
        /// <returns>Returns the request builder for chaining.</returns>
        /// <example><code>client.WithArguments(new { id = 14, name = "Joe" })</code></example>
        public virtual IRequest WithArguments(object arguments)
        {
            if (arguments == null)
                return this;

            KeyValuePair<string, object>[] args = arguments.GetKeyValueArguments().ToArray();

            this.Message.RequestUri = this.Message.RequestUri.WithArguments(this.Options.IgnoreNullArguments ?? false, args);
            return this;
        }

        /// <summary>Customize the underlying HTTP request message.</summary>
        /// <param name="request">The HTTP request message.</param>
        /// <returns>Returns the request builder for chaining.</returns>
        public virtual IRequest WithCustom(Action<HttpRequestMessage> request)
        {
            request(this.Message);
            return this;
        }

        /// <summary>Specify the token that can be used to cancel the async operation.</summary>
        /// <param name="cancellationToken">The cancellationtoken.</param>
        /// <returns>Returns the request builder for chaining.</returns>
        public virtual IRequest WithCancellationToken(CancellationToken cancellationToken)
        {
            this.CancellationToken = cancellationToken;
            return this;
        }


        /// <summary>Set options for this request.</summary>
        /// <param name="options">The options to set. (Fields set to <c>null</c> won't change the current value.)</param>
        public virtual IRequest WithOptions(RequestOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (options.IgnoreHttpErrors.HasValue)
                this.Options.IgnoreHttpErrors = options.IgnoreHttpErrors.Value;
            if (options.IgnoreNullArguments.HasValue)
                this.Options.IgnoreNullArguments = options.IgnoreNullArguments.Value;

            return this;
        }

        /// <summary>Specify the request coordinator for this request.</summary>
        /// <param name="requestCoordinator">The request coordinator</param>
        public virtual IRequest WithRequestCoordinator(IRequestCoordinator requestCoordinator)
        {
            this.RequestCoordinator = requestCoordinator;
            return this;
        }


        /// <summary>Get an object that waits for the completion of the request. This enables support for the <c>await</c> keyword.</summary>
        /// <example>
        /// <code>await client.PostAsync("api/ideas", idea);</code>
        /// <code>await client.GetAsync("api/ideas").AsString();</code>
        /// </example>
        public virtual TaskAwaiter<IResponse> GetAwaiter()
        {
            async Task<IResponse> Waiter() => await this.Execute().ConfigureAwait(false);
            return Waiter().GetAwaiter();
        }

        /// <summary>Asynchronously retrieve the HTTP response.</summary>
        /// <exception cref="HttpException">An error occurred processing the response.</exception>
        public virtual Task<IResponse> AsResponse()
        {
            return this.Execute();
        }

        /// <summary>Asynchronously retrieve the HTTP response.</summary>
        /// <exception cref="HttpException">An error occurred processing the response.</exception>
        public virtual async Task<HttpResponseMessage> AsMessage()
        {
            IResponse response = await this.AsResponse().ConfigureAwait(false);
            return response.Message;
        }

        /// <summary>Asynchronously retrieve the response body as a deserialized model.</summary>
        /// <typeparam name="T">The response model to deserialize into.</typeparam>
        /// <exception cref="HttpException">An error occurred processing the response.</exception>
        public virtual async Task<T> As<T>()
        {
            IResponse response = await this.AsResponse().ConfigureAwait(false);
            return await response.As<T>().ConfigureAwait(false);
        }

        /// <summary>Asynchronously retrieve the response body as a list of deserialized models.</summary>
        /// <typeparam name="T">The response model to deserialize into.</typeparam>
        /// <exception cref="HttpException">An error occurred processing the response.</exception>
        public virtual Task<T[]> AsArray<T>()
        {
            return this.As<T[]>();
        }

        /// <summary>Asynchronously retrieve the response body as an array of <see cref="byte"/>.</summary>
        /// <returns>Returns the response body, or <c>null</c> if the response has no body.</returns>
        /// <exception cref="HttpException">An error occurred processing the response.</exception>
        public virtual async Task<byte[]> AsByteArray()
        {
            IResponse response = await this.AsResponse().ConfigureAwait(false);
            return await response.AsByteArray().ConfigureAwait(false);
        }

        /// <summary>Asynchronously retrieve the response body as a <see cref="string"/>.</summary>
        /// <returns>Returns the response body, or <c>null</c> if the response has no body.</returns>
        /// <exception cref="HttpException">An error occurred processing the response.</exception>
        public virtual async Task<string> AsString()
        {
            IResponse response = await this.AsResponse().ConfigureAwait(false);
            return await response.AsString().ConfigureAwait(false);
        }

        /// <summary>Asynchronously retrieve the response body as a <see cref="Stream"/>.</summary>
        /// <returns>Returns the response body, or <c>null</c> if the response has no body.</returns>
        /// <exception cref="HttpException">An error occurred processing the response.</exception>
        public virtual async Task<Stream> AsStream()
        {
            IResponse response = await this.AsResponse().ConfigureAwait(false);
            return await response.AsStream().ConfigureAwait(false);
        }

        /// <summary>Get a raw JSON representation of the response, which can also be accessed as a <c>dynamic</c> value.</summary>
        public virtual async Task<JToken> AsRawJson()
        {
            IResponse response = await this.AsResponse().ConfigureAwait(false);
            return await response.AsRawJson().ConfigureAwait(false);
        }

        /// <summary>Get a raw JSON object representation of the response, which can also be accessed as a <c>dynamic</c> value.</summary>
        public virtual async Task<JObject> AsRawJsonObject()
        {
            IResponse response = await this.AsResponse().ConfigureAwait(false);
            return await response.AsRawJsonObject().ConfigureAwait(false);
        }

        /// <summary>Get a raw JSON array representation of the response, which can also be accessed as a <c>dynamic</c> value.</summary>
        public virtual async Task<JArray> AsRawJsonArray()
        {
            IResponse response = await this.AsResponse().ConfigureAwait(false);
            return await response.AsRawJsonArray().ConfigureAwait(false);
        }


        /// <summary>Execute the HTTP request and fetch the response.</summary>
        private async Task<IResponse> Execute()
        {
            // apply request filters
            if (this.Filters != null)
            {
                foreach (IHttpFilter filter in this.Filters)
                    filter.OnRequest(this);
            }

            // execute the request
            HttpResponseMessage responseMessage = this.RequestCoordinator != null
                ? await this.RequestCoordinator.ExecuteAsync(this, this.Dispatcher).ConfigureAwait(false)
                : await this.Dispatcher(this).ConfigureAwait(false);
            IResponse response = new Response(responseMessage, this.Formatters);

            // apply response filters
            if (this.Filters != null)
            {
                foreach (IHttpFilter filter in this.Filters)
                    filter.OnResponse(response, !(this.Options.IgnoreHttpErrors ?? false));
            }
            return response;
        }


    }
}
