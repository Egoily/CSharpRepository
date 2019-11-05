using ee.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.Models
{
    public static class GlobalConfigs
    {
        public static List<GuestInfo> Guests { get; set; }
        public static Configuration Configurations { get; set; }

    }
}
