using System.Windows;
using CommunityToolkit.Mvvm.Messaging;

namespace AdventCalender2022;

public partial class SubWindow : Window
{
    private readonly MyRecipient _recipient = new();
    public SubWindow()
    {
        InitializeComponent();

        //WeakReferenceMessenger.Default.Register(new MyRecipient()); ダメ！
        WeakReferenceMessenger.Default.Register(_recipient);

        WeakReferenceMessenger.Default.Register<SubWindow, MyMessage>(this, static (recipient, message) =>
        {
            System.Diagnostics.Debug.WriteLine($"Received: {message.Value}");
        });

        WeakReferenceMessenger.Default.Register<SubWindow, MyRequestMessage>(this, static (recipient, message) =>
        {
            message.Reply("Response from SubWindow.");
        });
    }

    ~SubWindow()
    {
        System.Diagnostics.Debug.WriteLine("Destructed");
    }
}
