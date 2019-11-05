using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Xml;

namespace Baidu.AI.Face
{
    public static class Configuration
    {
        //★★★你的APP_ID,在控制台应用列表中可以获取
        public const string APP_ID = "11403375";

        //★★★API_KEY,在控制台应用列表中可以获取
        public const string API_KEY = "OMQ0Z13yZBmdIwZq8I07EdrB";

        //★★★SECRET_KEY,在控制台应用列表中可以获取
        public const string SECRET_KEY = "24QNkF8PgRWQU7T34xc69MBytaquha18";

        //UNIT接口需要  Scene_ID 场景ID，可多场景Query，请从UNIT控制台-场景管理获取并填入
        public static readonly List<string> UNIT_SCENE_ID = new List<string>() { "" };

        //应用名称：BaiduAI开放平台  C#  SDK-CLI  快速部署底层框架

        //————————————————————————————————————

        //针对语音识别提供DirectX支持。 并附有基于事件的多线程版本、轮询录播版本。位于EventBase中

        //-该解决方案可以解决在PC端录制符合百度语音识别接口要求的音频文件。（采样位数：16Bit）

        //-需要确保运行环境存在至少DirectX支持。Win+R运行dxdiag查看相关信息。

        //-针对上层开发，使用DX无法调试的情况，请在应用配置文件app.config中，startup节点添加属性useLegacyV2RuntimeActivationPolicy="true" 并且将异常设置内的LoaderLock关闭即可

        //————————————————————————————————————

        //针对语音合成提供NAudio流播放支持。 本身就是多线程请求，内附事件。可选择使用

        //-该解决方案可以解决省略在PC端需要要将流保存成文件再播放文件的复杂过程，直接将接口回应的音频流从内存中播放。并且有独特的播放控制维护列表

        //————————————————————————————————————

        //针对人脸识别提供人脸预检测控件。

        //-该控件可有效的解决视频流挑战QPS的难题。

        //-控件拥有丰富的自定义参数和事件绑定，可自由控制上层的处理逻辑。

        //-详细的使用说明，请到人脸识别-AI.Face.Wpf.Control-VideoCaptureElement.xaml.cs查看

        //————————————————————————————————————

        //针对UNIT优化了数据结构 并提供有多线程事件版本

        //-使用多场景多SessionID的保存机制，可在上层自由控制每个会话的生命。将生命代入方法，可多场景续命交流

        public static readonly string ACCESS_TOKEN = GetAccessToken();

        //获取Access_Token私方法
        private static String GetAccessToken()
        {
            string FileName = System.Environment.CurrentDirectory + "\\ACCESS_TOKEN.xml";

            XmlDocument doc = new XmlDocument();

            //检测是否存在Token文件。不存在则新建。
            if (File.Exists(FileName))
            {
                doc.Load(FileName);
            }
            else
            {
                XmlNode node = doc.CreateXmlDeclaration("1.0", "utf-8", "");
                doc.AppendChild(node);

                XmlNode root = doc.CreateElement("root");
                doc.AppendChild(root);

                XmlNode QueryTime = doc.CreateNode(XmlNodeType.Element, "Query_Time", null);
                QueryTime.InnerText = new DateTime().ToString();
                root.AppendChild(QueryTime);

                XmlNode AccessToken = doc.CreateNode(XmlNodeType.Element, "Acess_Token", null);
                AccessToken.InnerText = "";
                root.AppendChild(AccessToken);

                XmlNode SessionKey = doc.CreateNode(XmlNodeType.Element, "Session_Key", null);
                SessionKey.InnerText = "";
                root.AppendChild(SessionKey);

                XmlNode Scope = doc.CreateNode(XmlNodeType.Element, "Scope", null);
                Scope.InnerText = "";
                root.AppendChild(Scope);

                XmlNode RefreshToken = doc.CreateNode(XmlNodeType.Element, "Refresh_Token", null);
                RefreshToken.InnerText = "";
                root.AppendChild(RefreshToken);

                XmlNode SessionSecret = doc.CreateNode(XmlNodeType.Element, "Session_Secret", null);
                SessionSecret.InnerText = "";
                root.AppendChild(SessionSecret);

                XmlNode ExpiresIn = doc.CreateNode(XmlNodeType.Element, "ExpiresIn", null);
                ExpiresIn.InnerText = "";
                root.AppendChild(ExpiresIn);

                XmlNode API_KEY = doc.CreateNode(XmlNodeType.Element, "API_KEY", null);
                API_KEY.InnerText = "";
                root.AppendChild(API_KEY);

                doc.Save(FileName);
            }

            //获取上次请求时间及对应的Token值
            string QueryTimeValue = doc.SelectSingleNode("root").ChildNodes[0].InnerText;
            string AccessTokenValue = doc.SelectSingleNode("root").ChildNodes[1].InnerText;
            string APIKEYValue = doc.SelectSingleNode("root").ChildNodes[7].InnerText;

            //上次请求时间
            DateTime dt = DateTime.Parse(QueryTimeValue);

            //现在系统时间
            DateTime now = DateTime.Now;

            //时间求差
            TimeSpan t = now.Subtract(dt);

            //如果距离上次请求已经有29天之久（Access_Token有效期30天），则重新请求刷新Access_Token
            if (t.Days >= 29 || API_KEY != APIKEYValue)
            {
                String authHost = "https://aip.baidubce.com/oauth/2.0/token";
                HttpClient client = new HttpClient();

                List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", API_KEY),
                    new KeyValuePair<string, string>("client_secret", SECRET_KEY)
                };
                HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;

                JObject result = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (!result.Properties().Any(i => i.Name == "error"))
                {
                    doc.SelectSingleNode("root").ChildNodes[0].InnerText = DateTime.Now.ToString();
                    AccessTokenValue = doc.SelectSingleNode("root").ChildNodes[1].InnerText = result["access_token"].ToString();
                    doc.SelectSingleNode("root").ChildNodes[2].InnerText = result["session_key"].ToString();
                    doc.SelectSingleNode("root").ChildNodes[3].InnerText = result["scope"].ToString();
                    doc.SelectSingleNode("root").ChildNodes[4].InnerText = result["refresh_token"].ToString();
                    doc.SelectSingleNode("root").ChildNodes[5].InnerText = result["session_secret"].ToString();
                    doc.SelectSingleNode("root").ChildNodes[6].InnerText = result["expires_in"].ToString();
                    doc.SelectSingleNode("root").ChildNodes[7].InnerText = API_KEY;

                    doc.Save(FileName);
                }
                else
                {
                    throw new Exception("在申请AccessToken的时候发生了异常，错误值为 :" + result["error"].ToString() + "，错误信息 :" + result["error_description"].ToString() + " 请检查。\n具体信息以百度文档文档中心(http://ai.baidu.com/docs#/Begin/top)为主");
                }
            }

            return AccessTokenValue;
        }
    }
}