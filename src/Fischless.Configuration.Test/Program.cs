namespace Fischless.Configuration.Test;

internal class Program
{
    public static void Main(string[] args)
    {
        string configExt = null!;

#if YAML || true
        ConfigurationManager.ConfigurationSerializer = new YamlConfigurationSerializer();
        configExt = ".yaml";
#elif JSON || false
        ConfigurationManager.ConfigurationSerializer = new JsonConfigurationSerializer();
        configExt = ".json";
#elif INI || false
        ConfigurationManager.ConfigurationSerializer = new IniConfigurationSerializer();
        configExt = ".ini";
#endif
        ConfigurationManager.Setup(ConfigurationSpecialPath.GetPath($"config{configExt}", "Fischless.Configuration.Test"));

        string lang1 = Configurations.Language.Get();
        Configurations.Language.Set("en");
        string lang2 = Configurations.Language.Get();

        ConfigurationManager.Save();
    }
}
