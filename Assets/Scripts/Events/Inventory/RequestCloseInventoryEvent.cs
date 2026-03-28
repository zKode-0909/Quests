using UnityEngine;

public struct RequestCloseInventoryEvent : IEvent
{
    public string EntityStableID;

    public RequestCloseInventoryEvent(string entityID)
    {
        EntityStableID = entityID;
    }
}