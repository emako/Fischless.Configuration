using System.Reflection;

namespace Fischless.Configuration.Test;

[Obfuscation]
public static class Configurations
{
    public static ConfigurationDefinition<string> Language { get; } = new(nameof(Language), string.Empty);
}
