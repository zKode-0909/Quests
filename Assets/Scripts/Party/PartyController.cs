using System.Collections.Generic;
using UnityEngine;

public class PartyController
{
    EventBinding<RequestStartPartyEvent> startPartyReqBinding;
    PartyFactory partyFactory;
    ActivePartyRegistry activePartyRegistry;

    public PartyController(PartyFactory factory,ActivePartyRegistry registry) {
        startPartyReqBinding = new EventBinding<RequestStartPartyEvent>(OnStartPartyRequest);
        partyFactory = factory;
        activePartyRegistry = registry;

        EventBus<RequestStartPartyEvent>.Register(startPartyReqBinding);
    }

    public void Dispose() {
        EventBus<RequestStartPartyEvent>.Deregister(startPartyReqBinding);
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
}
