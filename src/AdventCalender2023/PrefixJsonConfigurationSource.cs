using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace AdventCalender2023;

internal class PrefixJsonConfigurationSource : JsonConfigurationSource
{
    public PrefixJsonConfigurationSource() { }

    public string Prefix { get; init; } = string.Empty;

    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        EnsureDefaults(builder);
        return new PrefixJsonConfigurationProvider(Prefix, this);
    }
}
