using System.Collections.Generic;

using UnityEngine;

public class PartyController
{
    EventBinding<RequestStartPartyEvent> startPartyReqBinding;
    EventBinding<RequestPartyInviteEvent> partyInviteReqBinding;
    EventBinding<RequestJoinPartyEvent> joinPartyReqBinding;
    EventBinding<RemoveFromPartyRequestEvent> removeFromPartyReqBinding;
    PartyFactory partyFactory;
    ActivePartyRegistry activePartyRegistry;

    public PartyController(PartyFactory factory,ActivePartyRegistry registry) {
        startPartyReqBinding = new EventBinding<RequestStartPartyEvent>(OnStartPartyRequest);
        partyInviteReqBinding = new EventBinding<RequestPartyInviteEvent>(OnPartyInviteRequest);
        joinPartyReqBinding = new EventBinding<RequestJoinPartyEvent>(OnJoinPartyRequest);
        removeFromPartyReqBinding = new EventBinding<RemoveFromPartyRequestEvent>(OnRemoveFromPartyRequest);
        partyFactory = factory;
        activePartyRegistry = registry;

        EventBus<RequestStartPartyEvent>.Register(startPartyReqBinding);
        EventBus<RequestPartyInviteEvent>.Register(partyInviteReqBinding);
        EventBus<RequestJoinPartyEvent>.Register(joinPartyReqBinding);
        EventBus<RemoveFromPartyRequestEvent>.Register(removeFromPartyReqBinding);
    }

    public void Dispose() {
        EventBus<RequestStartPartyEvent>.Deregister(startPartyReqBinding);
        EventBus<RequestPartyInviteEvent>.Deregister(partyInviteReqBinding);
        EventBus<RequestJoinPartyEvent>.Deregister(joinPartyReqBinding);
        EventBus<RemoveFromPartyRequestEvent>.Deregister(removeFromPartyReqBinding);
    }

    void OnRemoveFromPartyRequest(RemoveFromPartyRequestEvent evt) {
        Debug.Log("about to try and remove from party");
        if (activePartyRegistry.TryGetActivePartyByMember(evt.toBeRemoved.EntityRuntimeID, out var party)) {
            Debug.Log("Got party");
            if (activePartyRegistry.TryUnregisterMember(evt.toBeRemoved.EntityRuntimeID)) {
                Debug.Log("removed member from party registry");
                if (party.TryRemoveMemberFromParty(evt.toBeRemoved.EntityRuntimeID)) {
                    Debug.Log($"{evt.toBeRemoved.EntityRuntimeID} has been removed from party: {party.GetPartyId()}");
                    evt.toBeRemoved.UpdatePartyStatus(false);
                    EventBus<RemoveFromPartyEvent>.Raise(new RemoveFromPartyEvent(evt.toBeRemoved));
                }
            }
        }
    }

    void OnJoinPartyRequest(RequestJoinPartyEvent evt) {
        if (activePartyRegistry.TryGetPartyById(evt.PartyRuntimeID, out var party)) {
            if (!party.CheckForPlayer(evt.Invitee.EntityRuntimeID))
            {
                if (activePartyRegistry.TryRegisterMemberToParty(evt.Invitee.EntityRuntimeID, party))
                {
                    if (party.TryAddMemberToParty(evt.Invitee.EntityRuntimeID))
                    {
                        Debug.Log($"{evt.Invitee.EntityRuntimeID} has joined the party with ID: {party.GetPartyId()} led by {party.GetPartyLeader()}");
                        evt.Invitee.UpdatePartyStatus(true);
                        EventBus<PartyJoinedEvent>.Raise(new PartyJoinedEvent(party.GetPartyLeader(), evt.Invitee));
                    }
                    else {
                        Debug.Log($"Could not add member to party");
                    }
                    
                }
            }
            else {
                Debug.Log($"invited player is already in this party");
            }
        }
    }

    void OnStartPartyRequest(RequestStartPartyEvent evt) {
        if (activePartyRegistry.TryGetActivePartyByMember(evt.EntityRuntimeID, out var party))
        {
            Debug.Log("This dude is in a party already, he can't start one");
            return;
        }
        else { 
            activePartyRegistry.TryRegisterParty(partyFactory.CreateParty(evt.EntityRuntimeID));
        }
    }

    void OnPartyInviteRequest(RequestPartyInviteEvent evt)
    {
        if (activePartyRegistry.TryGetActivePartyByMember(evt.Invitee.EntityRuntimeID, out _))
        {
            Debug.Log("Invited player is already in a party");
            return;
        }

        if (!activePartyRegistry.TryGetActivePartyByMember(evt.InviterEntityRuntimeID, out var p))
        {
            var newParty = partyFactory.CreateParty(evt.InviterEntityRuntimeID);

            if (!activePartyRegistry.TryRegisterParty(newParty))
            {
                Debug.Log("Failed to create/register a new party for inviter");
                return;
            }

            

            EventBus<SendPartyInviteEvent>.Raise(
            new SendPartyInviteEvent(newParty.GetPartyId(), evt.Invitee));

            return;
        }

        if (activePartyRegistry.TryGetActivePartyByMember(evt.InviterEntityRuntimeID,out var party)) {
            EventBus<SendPartyInviteEvent>.Raise(new SendPartyInviteEvent(party.GetPartyId(), evt.Invitee));
            return;
        }



        
    }
}
