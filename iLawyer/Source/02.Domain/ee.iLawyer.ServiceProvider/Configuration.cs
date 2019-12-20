using ee.Core.Framework.Configuration;

namespace ee.iLawyer.ServiceProvider
{
    public class Configuration
    {
        public static void Load()
        {
            ConfigManagement<ConfigData>.Default.Load();
        }
        public static void Save()
        {
            ConfigManagement<ConfigData>.Default.Save();
        }

        public static ConfigData Data
        {
            get
            {

                if (ConfigManagement<ConfigData>.Default.ConfigObject == null)
                {
                    Load();
                }
                return ConfigManagement<ConfigData>.Default.ConfigObject;
            }
        }
    }

}
