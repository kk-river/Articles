using System.Windows;
using CommunityToolkit.Mvvm.Messaging;

namespace AdventCalender2022;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        MainWindowViewModel viewModel = new();
        MainWindow window = new() { DataContext = viewModel, };

        window.Show();
        new SubWindow().Show();
    }
}
