using System.Diagnostics;
using CommunityToolkit.Mvvm.Messaging;

namespace AdventCalender2022;

internal class MyRecipient : IRecipient<MyMessage>
{
    public void Receive(MyMessage message)
    {
        Debug.WriteLine($"MyRecipient Received: {message.Value}");
    }
}
