using System.IO;
using Microsoft.Extensions.Configuration.Json;

namespace AdventCalender2023;

internal class PrefixJsonConfigurationProvider : JsonConfigurationProvider
{
    private readonly string _prefix;

    public PrefixJsonConfigurationProvider(string prefix, JsonConfigurationSource source) : base(source)
    {
        _prefix = prefix;
    }

    public override void Load(Stream stream)
    {
        base.Load(stream);

        Data = Data.ToDictionary(kvp => $"{_prefix}:{kvp.Key}", x => x.Value);
    }
}
