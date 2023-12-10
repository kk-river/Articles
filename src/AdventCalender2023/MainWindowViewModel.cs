using Microsoft.Extensions.Configuration;
using Reactive.Bindings;

namespace AdventCalender2023;

internal class MainWindowViewModel
{
    private readonly ReactivePropertySlim<ConfRecord[]> _loadedConfigs = new([]);
    private readonly IConfiguration _configuration;

    public IReadOnlyReactiveProperty<ConfRecord[]> LoadedConfigs => _loadedConfigs;

    public ReactiveCommandSlim ReloadCommand { get; } = new();

    public MainWindowViewModel(IConfiguration configuration)
    {
        _configuration = configuration;

        _loadedConfigs.Value =  configuration
            .AsEnumerable()
            .Select(x => new ConfRecord(x.Key, x.Value))
            .ToArray();

        ReloadCommand.Subscribe(Reload);
    }

    private void Reload()
    {
        _loadedConfigs.Value = _configuration
            .AsEnumerable()
            .Select(x => new ConfRecord(x.Key, x.Value))
            .ToArray();
    }
}


public record ConfRecord(string Key, string? Value);
