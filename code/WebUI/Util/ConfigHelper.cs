using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebUI.Util
{
    public class ConfigHelper
    {
        public int AddHours { get; set; }

        public ConfigHelper()
        {
            AddHours = Convert.ToInt32(ConfigurationManager.AppSettings["AddHours"]);
        }
    }
}