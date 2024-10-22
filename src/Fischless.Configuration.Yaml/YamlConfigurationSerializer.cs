using System;
using System.Diagnostics;
using System.IO;
using YamlDotNet.Serialization;

namespace Fischless.Configuration;

public class YamlConfigurationSerializer : IConfigurationSerializer
{
    public Lazy<Serializer> Serializer { get; protected private set; } = new(() => new());
    public Lazy<Deserializer> Deserializer { get; protected private set; } = new(() => new());

    public string SerializeObject<T>(T? obj)
    {
        return Serializer.Value.Serialize(obj);
    }

    public T? DeserializeObject<T>(string input)
    {
        return Deserializer.Value.Deserialize<T>(input);
    }

    public bool SerializeFile<T>(string fileName, T? obj)
    {
        bool ret = false;

        try
        {
            string str = Serializer.Value.Serialize(obj);
            using StreamWriter streamWriter = File.CreateText(fileName);

            streamWriter.Write(str);
            streamWriter.Flush();
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
            using StreamReader reader = new(fileName);
            info = Deserializer.Value.Deserialize<T>(reader);
        }
        catch (Exception e)
        {
            Debug.WriteLine($"[Fischless.Configuration] DeserializeFile failed, detail: {e}.");
        }
        return info;
    }
}
