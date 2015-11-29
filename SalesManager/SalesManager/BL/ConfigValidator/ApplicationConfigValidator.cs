using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL.ConfigValidator
{
    public class ApplicationConfigValidator
    {
        protected static IDictionary<string, Func<string, bool>> _keysValidators =
            new Dictionary<string, Func<string, bool>>
            {
                {"MaxDatabaseConnections", x => { return Int32.Parse(ConfigurationManager.AppSettings[x]) > 0; }},
                {"ServerFolder", x => { return Directory.Exists(ConfigurationManager.AppSettings[x]); }},
                {"NotAppropriateFilesFolder", x => { return Directory.Exists(ConfigurationManager.AppSettings[x]); }},
                {"ProcessedFilesFolder", x => { return Directory.Exists(ConfigurationManager.AppSettings[x]); }}
            };

        public void Validate()
        {
            DAL.DatabaseValidator.DatabaseValidator.Validate();

            foreach(var item in _keysValidators)
            {
                CheckConfigKey(item.Key, item.Value);
            }
        }

        protected void CheckConfigKey(string keyName, Func<string, bool> predicate)
        {
            var keyValue = ConfigurationManager.AppSettings[keyName];

            if (keyValue == null)
            {
                throw new ConfigurationErrorsException
                    (String.Format("Application config file don't have key '{0}'.", keyName));
            }

            if(!predicate(keyName))
            {
                throw new ConfigurationErrorsException
                    (String.Format("Application config file has incorrect value of key '{0}'.", keyName));
            }
        }
    }
}
