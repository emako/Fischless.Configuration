using IniParser;
using IniParser.Model;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Fischless.Configuration;

[Obsolete(nameof(NotImplementedException))]
public class IniConfigurationSerializer : IConfigurationSerializer
{
    public Lazy<FileIniDataParser> IniParser { get; protected private set; } = new(() => new());
    public string SectionName { get; set; } = "Settings";

    public string SerializeObject<T>(T? obj)
    {
        IniData iniData = new();
        Type type = typeof(T);

        foreach (PropertyInfo property in type.GetProperties())
        {
            string? value = property.GetValue(obj)?.ToString();
            if (value != null)
            {
                iniData[SectionName][property.Name] = value;
            }
        }

        string iniString = string.Empty;
        using StreamWriter stringWriter = new(iniString);
        IniParser.Value.WriteData(stringWriter, iniData);

        return stringWriter.ToString();
    }

    public T? DeserializeObject<T>(string input)
    {
        IniData iniData = IniParser.Value.Parser.Parse(input);
        T? obj = Activator.CreateInstance<T>();

        foreach (PropertyInfo property in typeof(T).GetProperties())
        {
            string value = iniData[SectionName][property.Name];
            if (!string.IsNullOrEmpty(value))
            {
                property.SetValue(obj, Convert.ChangeType(value, property.PropertyType));
            }
        }

        return obj;
    }

    public bool SerializeFile<T>(string fileName, T? obj)
    {
        bool ret = false;

        try
        {
            string iniString = SerializeObject(obj);
            File.WriteAllText(fileName, iniString);
            ret = true;
        }
        catch (Exception e)
        {
            Debug.WriteLine($"[Fischless.Configuration] SerializeFile failed, detail: {e}.");
        }
        return ret;
    }

    public T? DeserializeFile<T>(string fileName)
    {
        T? info = default;

        try
        {
            var iniString = File.ReadAllText(fileName);
            info = DeserializeObject<T>(iniString);
        }
        catch (Exception e)
        {
            Debug.WriteLine($"[Fischless.Configuration] DeserializeFile failed, detail: {e}.");
        }
        return info;
    }
}
