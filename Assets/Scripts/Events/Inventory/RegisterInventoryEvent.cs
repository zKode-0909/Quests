using UnityEngine;

public struct RegisterInventoryEvent : IEvent
{
    public string EntityStableID;

    public RegisterInventoryEvent(string entityID)
    {
        EntityStableID = entityID;
    }
}
