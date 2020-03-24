using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ee.Core.Net
{

    public enum NetScheme
    {
        [Description(@"http://")]
        Http,
        [Description(@"https://")]
        Https,
    }
    public enum MediaType
    {
        XWwwFormUrlencoded,
        Json,
        Xml,
    }
    //public enum HttpMethod
    //{
    //    [Description("GET")]
    //    GET,
    //    [Description("POST")]
    //    POST,
    //    [Description("PUT")]
    //    PUT,
    //    [Description("DELETE")]
    //    DELETE,
    //    [Description("HEAD")]
    //    HEAD,
    //    [Description("OPTIONS")]
    //    OPTIONS,
    //    [Description("TRACE")]
    //    TRACE,
    //    [Description("PATCH")]
    //    PATCH,
    //}
    public enum SignatureAlgorithm
    {
        None,
        /// <summary> Signature process version 1, with HmacSHA1. </summary>
        SHA1,
        /// <summary> Signature process version 1, with HmacSHA256. </summary>
        SHA256,
        /// <summary> Signature process version 3, with TC3-HMAC-SHA256. </summary>
        TC3SHA256,
    }
}
