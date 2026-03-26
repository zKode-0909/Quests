using UnityEngine;

public struct RemoveFromPartyRequestEvent : IEvent
{
    public ISelectable toBeRemoved;

    public RemoveFromPartyRequestEvent(ISelectable removed)
    {
        toBeRemoved = removed;
    }
}
