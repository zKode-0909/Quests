using UnityEngine;

public struct RequestStartPartyEvent : IEvent
{
    public int EntityRuntimeID;

    public RequestStartPartyEvent(int runtimeID)
    {
        EntityRuntimeID = runtimeID;
    }

}