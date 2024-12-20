﻿using System;

namespace Fischless.Configuration;

public sealed class ConfigurationDefinition<T>
{
    internal static ConfigurationCache Cache => ConfigurationManager.Cache;

    public string Name { get; }
    public T DefaultValue { get; }
    public Func<object, T>? Converter { get; }

    public ConfigurationDefinition(string name, T defaultValue, Func<object, T>? converter = null)
    {
        Name = name;
        DefaultValue = defaultValue;
        Converter = converter ?? DefaultConverter!;
    }

    public static T? DefaultConverter(object value)
    {
        if (value is null) return default;
        try
        {
            return ConfigurationManager.ConfigurationSerializer.DeserializeObject<T>(ConfigurationManager.ConfigurationSerializer.SerializeObject(value));
        }
        catch
        {
            try
            {
                return (T?)typeof(T).Assembly.CreateInstance(typeof(T).FullName!);
            }
            catch
            {
                return default;
            }
        }
    }

    public T Get()
    {
        return Cache.Get(this);
    }

    public void Set(T value)
    {
        Cache.Set(this, value);
    }

    public static implicit operator T(ConfigurationDefinition<T> self)
    {
        return self.Get();
    }

    public void Relay()
    {
        Cache.Set(this, Get());
    }
}
