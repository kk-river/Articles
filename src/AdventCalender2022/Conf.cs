using CommunityToolkit.Mvvm.ComponentModel;
using MemoryPack;

namespace AdventCalender2022;

[INotifyPropertyChanged]
[MemoryPackable]
internal partial class Conf
{
    [property: MemoryPackInclude]
    [ObservableProperty]
    private string _inputtedText = string.Empty;
}
