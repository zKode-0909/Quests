using UnityEngine;

public class PlayerPartyInviteHandler
{
    PlayerRegistry registry;
    EventBinding<SendPartyInviteEvent> sendPartyInviteBinding;

    public void Initialize(PlayerRegistry reg) { 
        registry = reg;

        sendPartyInviteBinding = new EventBinding<SendPartyInviteEvent>(HandlePartyInvite);

        EventBus<SendPartyInviteEvent>.Register(sendPartyInviteBinding);
    }


    public void HandlePartyInvite(SendPartyInviteEvent evt) {
        Debug.Log("handling party invite");
        if (registry.TryGetPlayer(evt.Invitee.EntityRuntimeID, out var player)) {
            Debug.Log("player invited found");
            player.HandlePartyInvite(evt.PartyID);
        }
    }
}
