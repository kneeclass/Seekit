using System;
using System.Configuration;

namespace Seekit.Settings {
    public class SeekitConfiguration : ConfigurationSection
    {
        private static SeekitConfiguration _configuration;

        /// <summary>
        /// Gets the configuration details from the web/app config file.
        /// </summary>
        /// <returns>SeekitConfiguration or null if not available in config</returns>
        public static SeekitConfiguration GetConfiguration() {
            return _configuration ?? (_configuration = ConfigurationManager.GetSection("seekit") as SeekitConfiguration);
        }

        [ConfigurationProperty("apiUrl", IsRequired = false)]
        public Uri ApiUrl { 
            get { return _uri ?? this["apiUrl"] as Uri;}
            set { _uri = value; }
        }
        private Uri _uri;

        [ConfigurationProperty("clientGuid", IsRequired = false)]
        public Guid ClientGuid { 
            get { return _clientGuid ?? (Guid)this["clientGuid"]; }
            set { _clientGuid = value; }
        }

        private Guid? _clientGuid;

    }
}
