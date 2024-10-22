using System;
using System.IO;

namespace Fischless.Configuration;

public static class ConfigurationSpecialPath
{
    private static readonly string _localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    public static string GetFolder(string? optionFolder = null)
    {
        return Path.Combine(_localApplicationData, optionFolder);
    }

    public static string GetPath(string? baseName = null, string? appName = null)
    {
        string configPath = Path.Combine(GetFolder(appName), baseName ?? string.Empty);

        if (!Directory.Exists(new FileInfo(configPath).DirectoryName))
        {
            Directory.CreateDirectory(new FileInfo(configPath).DirectoryName!);
        }
        return configPath;
    }
}
