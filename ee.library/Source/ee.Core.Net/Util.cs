using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
namespace ee.Core.Net
{
    public static class Util
    {
        public static string ToQueryString(KeyValuePair<string, object>[] arguments)
        {
            return string.Join("&",
               from argument in arguments
               where argument.Value != null
               let key = WebUtility.UrlEncode(argument.Key)
               let value = argument.Value != null ? WebUtility.UrlEncode(argument.Value.ToString()) : string.Empty
               select key + "=" + value
           );

        }
        public static string ToBody(KeyValuePair<string, object>[] arguments)
        {
            return JsonConvert.SerializeObject(arguments);
        }
        public static long GetTimestamp()
        {
#if NET45|| NET46||NET47||NET48
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            DateTime nowTime = DateTime.Now;
            long unixTime = (long)Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
            return unixTime;
#endif

#if NS2 || NETSTANDARD2_0||NETSTANDARD2_1
            DateTimeOffset expiresAtOffset = DateTimeOffset.Now;
            var totalSeconds = expiresAtOffset.ToUnixTimeMilliseconds();
            return totalSeconds;
#endif

        }
    }
}
