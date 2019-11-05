using ee.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingGreeting
{
    public static class GlobalConfigMgr
    {
        /// <summary>
        /// 明文配置文件路径
        /// </summary>
        private static string DataFile = System.Environment.CurrentDirectory + @"\data.json";
        private static string ConfigFile = System.Environment.CurrentDirectory + @"\config.json";
        private static string TableLayoutFile = System.Environment.CurrentDirectory + @"\tables.json";
        public static void LoadGuests()
        {
            if (!File.Exists(DataFile))
            {
                var fs = new FileStream(DataFile, FileMode.Create, FileAccess.ReadWrite);
                fs.Close();
            }

            var json = File.ReadAllText(DataFile);

            GlobalConfigs.Guests = JsonConvert.DeserializeObject<List<GuestInfo>>(json);


            if (GlobalConfigs.Guests == null)
            {
                GlobalConfigs.Guests = new List<GuestInfo>();
            }

        }
        public static void LoadConfig()
        {
            if (!File.Exists(ConfigFile))
            {
                var fs = new FileStream(ConfigFile, FileMode.Create, FileAccess.ReadWrite);
                fs.Close();
            }

            var json = File.ReadAllText(ConfigFile);

            GlobalConfigs.Configurations = JsonConvert.DeserializeObject<Configuration>(json);


            if (GlobalConfigs.Configurations == null)
            {
                GlobalConfigs.Configurations = new Configuration();
            }

        }
        public static void LoadTables()
        {
            if (!File.Exists(TableLayoutFile))
            {
                var fs = new FileStream(TableLayoutFile, FileMode.Create, FileAccess.ReadWrite);
                fs.Close();
            }

            var json = File.ReadAllText(TableLayoutFile);

            TableConfig.TableControls = JsonConvert.DeserializeObject<List<TableControlData>>(json);


            if (TableConfig.TableControls == null)
            {
                TableConfig.TableControls = new List<TableControlData>();
            }

        }
        public static void Load()
        {
            LoadConfig();
            LoadGuests();
            LoadTables();
        }
        public static void SaveGuests()
        {
            var jsonSetting = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            var json = JsonConvert.SerializeObject(GlobalConfigs.Guests, jsonSetting);

            File.WriteAllText(DataFile, json);
        }

        public static void SaveConfig()
        {
            var jsonSetting = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            var json = JsonConvert.SerializeObject(GlobalConfigs.Configurations, jsonSetting);

            File.WriteAllText(ConfigFile, json);
        }
        public static void SaveTables()
        {
            var jsonSetting = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            var json = JsonConvert.SerializeObject(TableConfig.TableControls, jsonSetting);

            File.WriteAllText(TableLayoutFile, json);
        }
    }
}
