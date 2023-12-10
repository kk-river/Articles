using System.ComponentModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AdventCalender2022;

[INotifyPropertyChanged]
[ObservableRecipient]
internal partial class MainWindowViewModel
{
    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
    [NotifyPropertyChangedFor(nameof(TextUpper))] //Textの変更時に、OnPropertyChanged("TextUpper")がコールされる
    [NotifyCanExecuteChangedFor(nameof(SyncMethodCommand))] //このプロパティが影響するCommandを指定する
    private string _text = string.Empty; //Text というプロパティが生成される

    public string TextUpper => _text.ToUpper(); //TextUpper というプロパティが生成される

    private bool CanExecuteSyncMethod => Text.Length > 0; //メソッドでも可。フィールドは不可
    //private bool CanExecuteSyncMethod() => Text.Length > 0; //メソッドでも可。フィールドは不可

    public MainWindowViewModel()
    {
        Messenger = WeakReferenceMessenger.Default;

        Messenger.Register<MainWindowViewModel, PropertyChangedMessage<string>>(this, static (recipient, message) =>
        {
            switch (message.PropertyName)
            {
                case nameof(Text):
                    //なんかする
                    break;
            }
        });
    }

    [RelayCommand]
    private void SendMessage()
    {
        Messenger.Send<MyMessage>(new("Mesage from MainWindowViewModel"));
    }

    [RelayCommand]
    private void SendRequest()
    {
        MyRequestMessage response = Messenger.Send<MyRequestMessage>(new(DateTime.Parse("2022/12/13")));

        Debug.WriteLine($"RequestedAt = {response.RequestedAt}, Response = {response.Response}");
    }

    [RelayCommand(CanExecute = nameof(CanExecuteSyncMethod))] //実行可否のプロバイダを指定する
    private void SyncMethod() //SyncMethodCommand が生成される
    {
    }

    [RelayCommand]
    private void SyncMethodWithParam(string str) //SyncMethodWithParamCommand が生成される
    {
    }

    [RelayCommand]
    private async Task AsyncMethodAsync() //AsyncMethodCommand が生成される
    {
    }

    [RelayCommand]
    private async Task AsyncMethodWithParamAsync(string str) //AsyncMethodWithParamCommand が生成される
    {
    }

    [RelayCommand]
    private async Task CancelableAsyncMethodAsync(CancellationToken token) //CancelableAsyncMethodCommand が生成される
    {
    }

    [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private async Task CancelableAsyncMethodWithParamAsync(string str, CancellationToken token) //CancelableAsyncMethodWithParamCommand が生成される
    {
        Text = str;
        await Task.Delay(int.MaxValue, token);
    }

    [RelayCommand]
    private void Cancel()
    {
        CancelableAsyncMethodWithParamCommand.Cancel();
    }

    partial void OnTextChanging(string value)
    {
        System.Diagnostics.Debug.WriteLine($"Changing: Text = {Text}, value = {value}");
    }

    partial void OnTextChanged(string value)
    {
        System.Diagnostics.Debug.WriteLine($"Changed: Text = {Text}, value = {value}");
    }
}
