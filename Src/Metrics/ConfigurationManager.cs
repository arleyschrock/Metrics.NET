using System.Linq;
using Microsoft.Extensions.Configuration;

namespace System.Configuration
{
    public static class ConfigurationManager
    {
        public static string PropertyName { get; set; } = "metrics";
        public static IConfiguration BaseConfiguration { get; set; }
        public static void Build(Action<IConfigurationBuilder> config)
        {
            var builder = new ConfigurationBuilder();
            config(builder);
            BaseConfiguration = builder.Build();
        }

        public class AppSettingsConfigurations
        {
            private IConfiguration _config;

            public AppSettingsConfigurations(IConfiguration config)
            {
                _config = config;
            }
            public string this[string key]
            {
                get
                {
                    key = key.Split('.').Last();
                    key = char.ToLower(key.First()) + key.Substring(1);
                    var section = _config.GetSection(PropertyName);
                    if (section != null)
                    {
                        return section.GetValue<string>(key);
                    }
                    return (string)null;
                }
            }
        }
        public static AppSettingsConfigurations AppSettings
        {
            get => new AppSettingsConfigurations(BaseConfiguration);
        }
    }
}
