using ee.Core.Utility;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace ee.Core.Net
{
    /// <summary> Signature helper class. </summary>
    public static class SignHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string SHA256Hex(string text)
        {
            using (SHA256 algo = SHA256.Create())
            {
                byte[] hashbytes = algo.ComputeHash(Encoding.UTF8.GetBytes(text));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashbytes.Length; ++i)
                {
                    builder.Append(hashbytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static byte[] HmacSHA256(byte[] key, byte[] msg)
        {
            using (HMACSHA256 mac = new HMACSHA256(key))
            {
                return mac.ComputeHash(msg);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="host"></param>
        /// <param name="arguments"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MakeSignPlainText(HttpMethod method, string host, KeyValuePair<string, object>[] arguments, string path)
        {
            return $"{method}{host}{path}?{Util.ToQueryString(arguments)}";
        }


        ///<summary>Generate signature.</summary>
        ///<param name="signKey">Credential SecretKey.</param>
        ///<param name="signatureAlgorithm">The algorithm to used for sign.</param>
        ///<param name="toBeSignedText">Plain text to be signed.</param>
        ///<returns>Signature.</returns>
        public static string Sign(string signKey, SignatureAlgorithm signatureAlgorithm, string toBeSignedText)
        {
            string signString = string.Empty;
            if (signatureAlgorithm == SignatureAlgorithm.SHA256)
            {
                using (HMACSHA256 mac = new HMACSHA256(Encoding.UTF8.GetBytes(signKey)))
                {
                    byte[] hash = mac.ComputeHash(Encoding.UTF8.GetBytes(toBeSignedText));
                    signString = Convert.ToBase64String(hash);
                }
            }
            else if (signatureAlgorithm == SignatureAlgorithm.SHA1)
            {
                using (HMACSHA1 mac = new HMACSHA1(Encoding.UTF8.GetBytes(signKey)))
                {
                    byte[] hash = mac.ComputeHash(Encoding.UTF8.GetBytes(toBeSignedText));
                    signString = Convert.ToBase64String(hash);
                }
            }
            else
            {
                throw new NotSupportedException($"Not support {signatureAlgorithm.ToString()} yet.");
            }
            return signString;
        }
    }
}