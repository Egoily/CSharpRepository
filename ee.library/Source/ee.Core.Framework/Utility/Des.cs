using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ee.Core.Framework.Utility
{
    public static class Des
    {
        /// <summary>  
        /// DES加密算法  
        /// sKey为8位或16位  
        /// </summary>  
        /// <param name="strToEncrypt">需要加密的字符串</param>  
        /// <param name="key">密钥</param>  
        /// <returns></returns>  
        public static string DesEncrypt(string strToEncrypt, string key)
        {
            if (string.IsNullOrEmpty(strToEncrypt)) return string.Empty;
            StringBuilder strRetValue = new StringBuilder();

            try
            {
                key = key.PadRight(8, '0');
                byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                byte[] keyIV = keyBytes;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(strToEncrypt);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();

                provider.Mode = CipherMode.ECB;//兼容其他语言的Des加密算法  
                provider.Padding = PaddingMode.Zeros;//自动补0

                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();

                //组织成16进制字符串            
                foreach (byte b in mStream.ToArray())
                {
                    strRetValue.AppendFormat("{0:X2}", b);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return strRetValue.ToString();
        }
        /// <summary>  
        /// DES解密算法  
        /// sKey为8位或16位  
        /// </summary>  
        /// <param name="strToDecrypt">需要解密的字符串</param>  
        /// <param name="key">密钥</param>  
        /// <returns></returns>  
        public static string DesDecrypt(string strToDecrypt, string key)
        {
            if (string.IsNullOrEmpty(strToDecrypt)) return string.Empty;
            string strRetValue = "";
            key = key.PadRight(8, '0');
            try
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                byte[] keyIV = keyBytes;


                //16进制转换为byte字节
                byte[] inputByteArray = new byte[strToDecrypt.Length / 2];
                for (int x = 0; x < strToDecrypt.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(strToDecrypt.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }

                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();

                provider.Mode = CipherMode.ECB;//兼容其他语言的Des加密算法  
                provider.Padding = PaddingMode.Zeros;//自动补0  

                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();

                strRetValue = Encoding.UTF8.GetString(mStream.ToArray()).TrimEnd('\0');
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return strRetValue;
        }
    }
}
