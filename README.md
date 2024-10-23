[![NuGet](https://img.shields.io/nuget/v/Fischless.Configuration.svg)](https://nuget.org/packages/Fischless.Configuration) [![GitHub license](https://img.shields.io/github/license/emako/Fischless.Configuration)](https://github.com/emako/Fischless.Configuration/blob/master/LICENSE) [![Actions](https://github.com/emako/Fischless.Configuration/actions/workflows/library.nuget.yml/badge.svg)](https://github.com/emako/Fischless.Configuration/actions/workflows/library.nuget.yml)

# Fischless.Configuration

A simple configuration library used for [Fischless](https://github.com/GenshinMatrix/Fischless).

The library may not offer the highest performance, but it is highly convenient to use.

Install `ConfigurationSerializer` from nuget:

| Name                         | Nuget                                                        |
| ---------------------------- | ------------------------------------------------------------ |
| Fischless.Configuration.Json | [![NuGet](https://img.shields.io/nuget/v/Fischless.Configuration.Json.svg)](https://nuget.org/packages/Fischless.Configuration.Json) |
| Fischless.Configuration.Yaml | [![NuGet](https://img.shields.io/nuget/v/Fischless.Configuration.Yaml.svg)](https://nuget.org/packages/Fischless.Configuration.Yaml) |
| Fischless.Configuration.Ini  | Not available                                                |

Another configuration library [here](https://github.com/lemutec/LyricStudio/tree/master/src/Desktop/LyricStudio/Core/Configuration) used for LyricStudio.

## Usage

1. Definition of yours in `Configurations.cs`.

```c#
public static class Configurations
{
    public static ConfigurationDefinition<string> Language { get; } = new(nameof(Language), string.Empty);
}
```

2. Use `ConfigurationManager` to read and write configurations file.

```c#
// Configurations setup of ConfigurationSerializer and FileName.
ConfigurationManager.ConfigurationSerializer = new JsonConfigurationSerializer(); // YamlConfigurationSerializer
ConfigurationManager.Setup(ConfigurationSpecialPath.GetPath($"config.yaml", "yourAppName"));

// Configurations Getter and Setter
string lang1 = Configurations.Language.Get();
Configurations.Language.Set("en");
string lang2 = Configurations.Language.Get();

// Save Configurations into your file.
ConfigurationManager.Save();
```

