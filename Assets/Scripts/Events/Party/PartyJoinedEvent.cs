using UnityEngine;

public struct PartyJoinedEvent : IEvent
{
    public int OwnerEntityRuntimeID;
    public ISelectable Joiner;

    public PartyJoinedEvent(int owner, ISelectable joiner)
    {
        OwnerEntityRuntimeID = owner;
        Joiner = joiner;
    }

}
