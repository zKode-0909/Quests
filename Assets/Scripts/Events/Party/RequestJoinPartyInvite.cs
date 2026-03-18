using UnityEngine;

public struct RequestJoinPartyEvent : IEvent
{
    public int PartyRuntimeID;
    public int InviteeEntityRuntimeID;

    public RequestJoinPartyEvent(int party, int invitee)
    {
        PartyRuntimeID = party;
        InviteeEntityRuntimeID = invitee;
    }

}