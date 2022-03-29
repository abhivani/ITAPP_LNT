using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace ITApp.Utility
{
    public class AppSetting
    {
        public static string authenticationMode
        {
            get { return WebConfigurationManager.AppSettings["auth"]; }
        }
        public static string FileAllowedExtensions
        {
            get { return WebConfigurationManager.AppSettings["FileAllowedExtensions"]; }
        }
    }
}
