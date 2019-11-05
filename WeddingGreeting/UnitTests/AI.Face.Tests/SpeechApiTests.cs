using Baidu.AI.Face;
using Baidu.AI.Face.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Drawing;

namespace AI.Face.Tests
{
    [TestClass()]
    public class SpeechApiTests
    {
        [TestInitialize()]
        public void Initialize()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        [TestMethod()]
        public void AsrRecognizeTest()
        {
            SpeechApi.Recognize("8k.pcm");
            SpeechApi.Recognize("8k.wav");
        }

    }
}