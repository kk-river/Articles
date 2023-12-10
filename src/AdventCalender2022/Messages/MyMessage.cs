using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AdventCalender2022;

internal class MyMessage : ValueChangedMessage<string>
{
    public MyMessage(string value) : base(value) { }
}
