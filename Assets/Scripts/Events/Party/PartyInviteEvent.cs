using UnityEngine;

public struct RequestPartyInviteEvent : IEvent
{
    public int InviterEntityRuntimeID;
    public ISelectable Invitee;

    public RequestPartyInviteEvent(int inviter,ISelectable invitee)
    {
        InviterEntityRuntimeID = inviter;
        Invitee = invitee;
    }

}
