using UnityEngine;

public struct RegisterInventoryEvent : IEvent
{
    public int EntityRuntimeID;

    public RegisterInventoryEvent(int entityID)
    {
        EntityRuntimeID = entityID;
    }
}
