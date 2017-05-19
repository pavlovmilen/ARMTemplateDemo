using System.Configuration;
using System.Linq;

namespace MyCoolWebApp.Services
{
    public class AppSettingsWebConfigReader
    {
        public string ReadProperty(string key)
        {
            var returnValue = string.Empty;

            if (HasPropertyForKey(key))
            {
                returnValue = ConfigurationManager.AppSettings[key];
            }

            return returnValue;
        }

        private bool HasPropertyForKey(string key)
        {
            return !string.IsNullOrEmpty(key) && ConfigurationManager.AppSettings.AllKeys.Contains(key);
        }
    }
}