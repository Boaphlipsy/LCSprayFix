using BepInEx.Configuration;

namespace LCSprayFix
{
    public class PluginConfig
    {
        public bool DEV_MODE { get; private set; }
        public ConfigFile Config { get; private set; }

        public PluginConfig(ConfigFile cfg)
        {
            Config = cfg;

            InitConfig();
        }

        private T ConfigEntry<T>(string section, string key, T defaultVal, string description)
        {
            return Config.Bind(section, key, defaultVal, description).Value;
        }

        private void InitConfig()
        {
            DEV_MODE = ConfigEntry("Other", "Dev Mode", false, "Enable this for some extra logging.");
        }
    }
}