using UnityEngine;

public struct RemoveFromPartyEvent : IEvent
{
    public ISelectable toBeRemoved;

    public RemoveFromPartyEvent(ISelectable removed) {
        toBeRemoved = removed;
    }
}
