using UnityEngine;

public struct RequestJoinPartyEvent : IEvent
{
    public int PartyRuntimeID;
    public ISelectable Invitee;

    public RequestJoinPartyEvent(int party, ISelectable invitee)
    {
        PartyRuntimeID = party;
        Invitee = invitee;
    }

}