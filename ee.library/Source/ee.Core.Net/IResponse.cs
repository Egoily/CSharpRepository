﻿using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace ee.Core.Net
{
    /// <summary>Asynchronously parses an HTTP response.</summary>
    public interface IResponse
    {
        /*********
        ** Accessors
        *********/
        /// <summary>Whether the HTTP response was successful.</summary>
        bool IsSuccessStatusCode { get; }

        /// <summary>The HTTP status code.</summary>
        HttpStatusCode Status { get; }

        /// <summary>The underlying HTTP response message.</summary>
        HttpResponseMessage Message { get; }

        /// <summary>The formatters used for serializing and deserializing message bodies.</summary>
        MediaTypeFormatterCollection Formatters { get; }


        /*********
        ** Methods
        *********/
        /// <summary>Asynchronously retrieve the response body as a deserialized model.</summary>
        /// <typeparam name="T">The response model to deserialize into.</typeparam>
        /// <exception cref="HttpException">An error occurred processing the response.</exception>
        Task<T> As<T>();

        /// <summary>Asynchronously retrieve the response body as a list of deserialized models.</summary>
        /// <typeparam name="T">The response model to deserialize into.</typeparam>
        /// <exception cref="HttpException">An error occurred processing the response.</exception>
        Task<T[]> AsArray<T>();

        /// <summary>Asynchronously retrieve the response body as an array of <see cref="byte"/>.</summary>
        /// <returns>Returns the response body, or <c>null</c> if the response has no body.</returns>
        /// <exception cref="HttpException">An error occurred processing the response.</exception>
        Task<byte[]> AsByteArray();

        /// <summary>Asynchronously retrieve the response body as a <see cref="string"/>.</summary>
        /// <returns>Returns the response body, or <c>null</c> if the response has no body.</returns>
        /// <exception cref="HttpException">An error occurred processing the response.</exception>
        Task<string> AsString();

        /// <summary>Asynchronously retrieve the response body as a <see cref="Stream"/>.</summary>
        /// <returns>Returns the response body, or <c>null</c> if the response has no body.</returns>
        /// <exception cref="HttpException">An error occurred processing the response.</exception>
        Task<Stream> AsStream();

        /// <summary>Get a raw JSON representation of the response, which can also be accessed as a <c>dynamic</c> value.</summary>
        Task<JToken> AsRawJson();

        /// <summary>Get a raw JSON object representation of the response, which can also be accessed as a <c>dynamic</c> value.</summary>
        Task<JObject> AsRawJsonObject();

        /// <summary>Get a raw JSON array representation of the response, which can also be accessed as a <c>dynamic</c> value.</summary>
        Task<JArray> AsRawJsonArray();
    }
}
