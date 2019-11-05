using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingGreeting
{
    public class SpeechProvider
    {
        private static System.Speech.Synthesis.SpeechSynthesizer voice = null;

        private static System.Speech.Synthesis.SpeechSynthesizer Instance
        {
            get { return voice ?? (voice = new System.Speech.Synthesis.SpeechSynthesizer() { Rate = 1, Volume = 100 }); }
        }
        /// <summary>
        /// 朗读文字
        /// </summary>
        /// <param name="text"></param>
        public static void Speech(string text)
        {
            try
            {
                Instance.Speak(text);  //同步朗读

                //using (var voice = new System.Speech.Synthesis.SpeechSynthesizer())  //创建语音实例
                //{
                //    voice.Rate = 1; //设置语速,[-10,10]
                //    voice.Volume = 100; //设置音量,[0,100]
                //    voice.Speak(text);  //同步朗读
                //    //voice.SpeakAsync(text);  //播放指定的字符串,这是异步朗读
                //}
            }
            catch (Exception ex)
            {
            }
        }

        public static void Dispose()
        {
            voice?.Dispose();
        }



    }
}
