using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ee.Core.Net
{
    public class HttpInvoker
    {

        public enum MediaType
        {
            XWwwFormUrlencoded,
            Json,
            Xml,
        }
        public enum HttpRequestMethod
        {
            GET,
            POST,
            PUT,
            DELETE,
            HEAD,
            OPTIONS,
            TRACE,
            PATCH,
        }

        private static readonly string DefaultUserAgent =
            "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static string ToString(HttpWebResponse response)
        {
            Stream stream = response.GetResponseStream();
            if (stream != null)
            {
                StreamReader sr = new StreamReader(stream);
                string result = sr.ReadToEnd();

                stream.Close();
                stream.Dispose();
                sr.Close();
                sr.Dispose();

                return result;
            }
            return string.Empty;
        }
        /// <summary>
        /// 屏蔽https的服务器证书验证,总是返回true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors errors)
        {
            return true; //总是接受  
        }

        private static string ToContentTypeString(MediaType type)
        {
            var typeString = "";
            switch (type)
            {
                case MediaType.XWwwFormUrlencoded:
                    typeString = "application/x-www-form-urlencoded";
                    break;
                case MediaType.Json:
                    typeString = "application/json";
                    break;
                case MediaType.Xml:
                    typeString = "application/xml";
                    break;
                default:
                    typeString = "application/x-www-form-urlencoded";
                    break;
            }
            return typeString;

        }
        public static HttpWebResponse Request(HttpRequestMethod method, string uri, string para = null, string body = null, int? timeout = null, MediaType mediaType = MediaType.Json, Encoding encoding = null, string userAgent = null, CookieCollection cookies = null)
        {
            if (string.IsNullOrEmpty(uri))
            {
                throw new ArgumentNullException("uri");
            }
            if (para != null)
            {
                uri = uri.Contains('?') ? uri.Trim('&') : uri + "?";
                uri += para.Trim('&');
            }

            HttpWebRequest request = null;

            if (uri.ToLower().StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback =
                    new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(uri) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(uri) as HttpWebRequest;
            }
            request.Method = method.ToString();
            request.ContentType = ToContentTypeString(mediaType);
            request.UserAgent = !string.IsNullOrEmpty(userAgent) ? userAgent : DefaultUserAgent;
            request.Timeout = timeout ?? 0;

            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }

            if (!string.IsNullOrEmpty(body))
            {
                if (encoding == null)
                {
                    encoding = Encoding.UTF8;
                }
                byte[] data = encoding.GetBytes(body);
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            else
            {
                request.ContentLength = 0;
            }
            return request.GetResponse() as HttpWebResponse;
        }

        public static async Task<HttpWebResponse> RequestAsync(HttpRequestMethod method, string uri, string para = null, string body = null, int? timeout = null, MediaType mediaType = MediaType.Json, Encoding encoding = null, string userAgent = null, CookieCollection cookies = null)
        {
            if (string.IsNullOrEmpty(uri))
            {
                throw new ArgumentNullException("uri");
            }
            if (para != null)
            {
                uri = uri.Contains('?') ? uri.Trim('&') : uri + "?";
                uri += para.Trim('&');
            }

            HttpWebRequest request = null;

            if (uri.ToLower().StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback =
                    new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(uri) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(uri) as HttpWebRequest;
            }
            request.Method = method.ToString();
            request.ContentType = ToContentTypeString(mediaType);
            request.UserAgent = !string.IsNullOrEmpty(userAgent) ? userAgent : DefaultUserAgent;
            request.Timeout = timeout ?? 0;

            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }

            if (!string.IsNullOrEmpty(body))
            {
                if (encoding == null)
                {
                    encoding = Encoding.UTF8;
                }
                byte[] data = encoding.GetBytes(body);
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            else
            {
                request.ContentLength = 0;
            }
            return await request.GetResponseAsync() as HttpWebResponse;
        }



        public static HttpWebResponse Get(string uri, string para = null, int? timeout = null, Encoding encoding = null, string userAgent = null, CookieCollection cookies = null)
        {
            return Request(HttpRequestMethod.GET, uri, para, null, timeout, MediaType.XWwwFormUrlencoded, encoding, userAgent, cookies);
        }
        public static async Task<HttpWebResponse> GetAsync(string uri, string para = null, int? timeout = null, Encoding encoding = null, string userAgent = null, CookieCollection cookies = null)
        {
            return await RequestAsync(HttpRequestMethod.GET, uri, para, null, timeout, MediaType.XWwwFormUrlencoded, encoding, userAgent, cookies);
        }
        public static HttpWebResponse Post(string uri, string para = null, string body = null, int? timeout = null, MediaType mediaType = MediaType.Json, Encoding encoding = null, string userAgent = null, CookieCollection cookies = null)
        {
            return Request(HttpRequestMethod.POST, uri, para, body, timeout, mediaType, encoding, userAgent, cookies);
        }
        public static async Task<HttpWebResponse> PostAsync(string uri, string para = null, string body = null, int? timeout = null, MediaType mediaType = MediaType.Json, Encoding encoding = null, string userAgent = null, CookieCollection cookies = null)
        {
            return await RequestAsync(HttpRequestMethod.POST, uri, para, body, timeout, mediaType, encoding, userAgent, cookies);
        }



        public static string GetToString(string uri, string para = null, int? timeout = null, Encoding encoding = null, string userAgent = null, CookieCollection cookies = null)
        {
            using (var response = Get(uri, para, timeout, encoding, userAgent, cookies))
            {
                return ToString(response);
            }
        }
        public static async Task<string> GetToStringAsync(string uri, string para = null, int? timeout = null, Encoding encoding = null, string userAgent = null, CookieCollection cookies = null)
        {
            using (var response = await GetAsync(uri, para, timeout, encoding, userAgent, cookies))
            {
                return ToString(response);
            }
        }

        public static string PostToString(string uri, string para = null, string body = null, int? timeout = null, MediaType mediaType = MediaType.Json, Encoding encoding = null, string userAgent = null, CookieCollection cookies = null)
        {
            using (var response = Post(uri, para, body, timeout, mediaType, encoding, userAgent, cookies))
            {
                return ToString(response);
            }
        }
        public static async Task<string> PostToStringAsync(string uri, string para = null, string body = null, int? timeout = null, MediaType mediaType = MediaType.Json, Encoding encoding = null, string userAgent = null, CookieCollection cookies = null)
        {
            using (var response = await PostAsync(uri, para, body, timeout, mediaType, encoding, userAgent, cookies))
            {
                return ToString(response);
            }
        }





        public static bool TestNetworkStatus()
        {
            var urls = new string[] { "http://www.baidu.com", "http://www.qq.com/" };
            int count = 0;
            foreach (var url in urls)
            {
                try
                {
                    using (var resp = Request(HttpRequestMethod.HEAD, url))
                    {
                        if (resp.StatusCode == HttpStatusCode.OK)
                        {
                            count++;
                        }
                    }
                }
                catch { }
            }
            return count > 0;
        }



    }
}
