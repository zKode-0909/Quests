using UnityEngine;

public class SimPlayer : Player
{
    public override void HandlePartyInvite(int partyID)
    {
        Debug.Log("sending party join request");
        EventBus<RequestJoinPartyEvent>.Raise(new RequestJoinPartyEvent(partyID,entityRuntimeID));
    }
}
