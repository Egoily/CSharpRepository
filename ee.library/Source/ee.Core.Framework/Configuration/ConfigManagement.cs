using ee.Core.Framework.Utility;
using Newtonsoft.Json;
using System.IO;

namespace ee.Core.Framework.Configuration
{
    public class ConfigManagement<T> where T : new()
    {

        public ConfigManagement(string configFile, string desKey)
        {
            _configFile = configFile;
            _desKey = desKey;
            if (!string.IsNullOrEmpty(_desKey))
            {
                _desConfigFile = _configFile + ".des";
            }
        }


        private static readonly object CreationLock = new object();
        private static ConfigManagement<T> _defaultInstance;

        public static ConfigManagement<T> Default
        {
            get
            {
                if (_defaultInstance == null)
                {
                    lock (CreationLock)
                    {
                        if (_defaultInstance == null)
                        {
                            _defaultInstance = new ConfigManagement<T>(System.Environment.CurrentDirectory + @"\data.json", "Ego");
                        }
                    }
                }

                return _defaultInstance;
            }
        }















        private string _configFile { get; set; }

        private string _desConfigFile { get; set; }
        private string _desKey { get; set; }
        public T ConfigObject { get; set; }



        /// <summary>
        /// 加载系统配置
        /// </summary>
        public void Load()
        {
            #region 加载系统配置
            if (!File.Exists(_desConfigFile))
            {
                var fs = new FileStream(_desConfigFile, FileMode.Create, FileAccess.ReadWrite);
                fs.Close();
            }
            //如果存在明文配置文件,则读取明文文件所在配置
            if (File.Exists(_configFile))
            {
                var deText = File.ReadAllText(_configFile);
                var enText = Utility.Des.DesEncrypt(deText, _desKey);
                File.WriteAllText(_desConfigFile, enText);
                //读取后删除明文配置文件
                File.Delete(_configFile);
            }
            //读出密文
            var encryptText = File.ReadAllText(_desConfigFile);
            //解密
            var decryptText = Des.DesDecrypt(encryptText, _desKey);
            ConfigObject = JsonConvert.DeserializeObject<T>(decryptText);


            if (ConfigObject == null)
            {
                ConfigObject = new T();
            }
            #endregion
        }
        /// <summary>
        /// 保存配置数据到文件
        /// </summary>
        public void Save()
        {
            var jsonSetting = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            var json = JsonConvert.SerializeObject(ConfigObject, jsonSetting);
            //加密后保存
            var enText = Des.DesEncrypt(json, _desKey);
            File.WriteAllText(_desConfigFile, enText);
        }


    }
}
