using System;
using System.Collections.Generic;

namespace ee.iLawyer.ServiceProvider
{
    [Serializable]
    public class ConfigData
    {
        public Dictionary<string, string> Accounts { get; set; }
        public string CurrentAccountName { get; set; }

        public ConfigData()
        {
            Accounts = new Dictionary<string, string>();
        }

    }
}
