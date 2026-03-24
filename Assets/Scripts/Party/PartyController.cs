using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PartyController
{
    EventBinding<RequestStartPartyEvent> startPartyReqBinding;
    EventBinding<RequestPartyInviteEvent> partyInviteReqBinding;
    EventBinding<RequestJoinPartyEvent> joinPartyReqBinding;
    PartyFactory partyFactory;
    ActivePartyRegistry activePartyRegistry;

    public PartyController(PartyFactory factory,ActivePartyRegistry registry) {
        startPartyReqBinding = new EventBinding<RequestStartPartyEvent>(OnStartPartyRequest);
        partyInviteReqBinding = new EventBinding<RequestPartyInviteEvent>(OnPartyInviteRequest);
        joinPartyReqBinding = new EventBinding<RequestJoinPartyEvent>(OnJoinPartyRequest);
        partyFactory = factory;
        activePartyRegistry = registry;

        EventBus<RequestStartPartyEvent>.Register(startPartyReqBinding);
        EventBus<RequestPartyInviteEvent>.Register(partyInviteReqBinding);
        EventBus<RequestJoinPartyEvent>.Register(joinPartyReqBinding);
    }

    public void Dispose() {
        EventBus<RequestStartPartyEvent>.Deregister(startPartyReqBinding);
        EventBus<RequestPartyInviteEvent>.Deregister(partyInviteReqBinding);
        EventBus<RequestJoinPartyEvent>.Deregister(joinPartyReqBinding);
    }

    void OnJoinPartyRequest(RequestJoinPartyEvent evt) {
        if (activePartyRegistry.TryGetPartyById(evt.PartyRuntimeID, out var party)) {
            if (!party.CheckForPlayer(evt.Invitee.EntityRuntimeID))
            {
                if (activePartyRegistry.TryRegisterMemberToParty(evt.Invitee.EntityRuntimeID, party))
                {
                    Debug.Log($"{evt.Invitee.EntityRuntimeID} has joined the party with ID: {party.GetPartyId()} led by {party.GetPartyLeader()}");
                    EventBus<PartyJoinedEvent>.Raise(new PartyJoinedEvent(party.GetPartyLeader(), evt.Invitee));
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

        if (!activePartyRegistry.TryGetActivePartyByMember(evt.InviterEntityRuntimeID, out var party))
        {
            var newParty = partyFactory.CreateParty(evt.InviterEntityRuntimeID);

            if (!activePartyRegistry.TryRegisterParty(newParty))
            {
                Debug.Log("Failed to create/register a new party for inviter");
                return;
            }

            party = newParty;
        }

        EventBus<SendPartyInviteEvent>.Raise(
            new SendPartyInviteEvent(party.GetPartyId(), evt.Invitee));
    }
}
