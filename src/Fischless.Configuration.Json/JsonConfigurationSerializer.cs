using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;

namespace Fischless.Configuration;

public class JsonConfigurationSerializer : IConfigurationSerializer
{
    public Formatting Formatting { get; set; } = Formatting.Indented;

    public string SerializeObject<T>(T? obj)
    {
        return JsonConvert.SerializeObject(obj, Formatting);
    }

    public T? DeserializeObject<T>(string input)
    {
        return JsonConvert.DeserializeObject<T>(input);
    }

    public bool SerializeFile<T>(string fileName, T? obj)
    {
        bool ret = false;

        try
        {
            string jsonString = SerializeObject(obj);
            File.WriteAllText(fileName, jsonString);
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
            string jsonString = File.ReadAllText(fileName);
            info = DeserializeObject<T>(jsonString);
        }
        catch (Exception e)
        {
            Debug.WriteLine($"[Fischless.Configuration] DeserializeFile failed, detail: {e}.");
        }
        return info;
    }
}
