namespace Fischless.Configuration;

public interface IConfigurationSerializer
{
    public string SerializeObject<T>(T? obj);

    public T? DeserializeObject<T>(string input);

    public bool SerializeFile<T>(string fileName, T? obj);

    public T? DeserializeFile<T>(string fileName);
}
