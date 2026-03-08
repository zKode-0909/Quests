using UnityEngine;

public class PartyController
{
    EventBinding<RequestStartPartyEvent> startPartyReqBinding;

    public PartyController() {
        startPartyReqBinding = new EventBinding<RequestStartPartyEvent>(OnStartPartyRequest);

        EventBus<RequestStartPartyEvent>.Register(startPartyReqBinding);
    }

    public void Dispose() {
        EventBus<RequestStartPartyEvent>.Deregister(startPartyReqBinding);
    }

    void OnStartPartyRequest() { }
}
