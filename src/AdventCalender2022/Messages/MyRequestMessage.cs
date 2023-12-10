using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AdventCalender2022;

internal class MyRequestMessage : RequestMessage<string>
{
    public DateTime RequestedAt { get; }

    public MyRequestMessage(DateTime requestedAt)
    {
        RequestedAt = requestedAt;
    }
}
