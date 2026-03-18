using UnityEngine;

public struct SendPartyInviteEvent : IEvent
{
    public int PartyID;
    public int InviteeEntityRuntimeID;

    public SendPartyInviteEvent(int party, int invitee)
    {
        PartyID = party;
        InviteeEntityRuntimeID = invitee;
    }

}