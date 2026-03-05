using UnityEngine;

public struct RequestOpenInventoryEvent : IEvent
{
    public int EntityRuntimeID;

    public RequestOpenInventoryEvent(int entityID)
    {
        EntityRuntimeID = entityID;
    }
}
