using UnityEngine;

public struct RequestPartyInviteEvent : IEvent
{
    public int InviterEntityRuntimeID;
    public int InviteeEntityRuntimeID;

    public RequestPartyInviteEvent(int inviter,int invitee)
    {
        InviterEntityRuntimeID = inviter;
        InviteeEntityRuntimeID = invitee;
    }

}
