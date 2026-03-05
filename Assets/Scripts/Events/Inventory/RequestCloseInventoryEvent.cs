using UnityEngine;

public struct RequestCloseInventoryEvent : IEvent
{
    public int EntityRuntimeID;

    public RequestCloseInventoryEvent(int entityID)
    {
        EntityRuntimeID = entityID;
    }
}