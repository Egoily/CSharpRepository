using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baidu.AI.Face
{
    public class SpeechApi
    {

        static string appId = "9216530";
        static string apiKey = "ySdumtGug55Z6sSVprE1reNO";
        static string secretKey = "3f1ec51bc010c9b7145774042142e0b4";
        static private Baidu.Aip.Speech.Asr client = new Baidu.Aip.Speech.Asr(appId, apiKey, secretKey);

        public static void Recognize(string fileName)
        {
            var data = File.ReadAllBytes(fileName);
            client.Timeout = 120000; // 若语音较长，建议设置更大的超时时间. ms
            var result = client.Recognize(data, "pcm", 16000);
            Console.Write(result);
        }
    }
}
