using UnityEngine;

public struct SendPartyInviteEvent : IEvent
{
    public int PartyID;
    public ISelectable Invitee;

    public SendPartyInviteEvent(int party, ISelectable invitee)
    {
        PartyID = party;
        Invitee = invitee;
    }

}