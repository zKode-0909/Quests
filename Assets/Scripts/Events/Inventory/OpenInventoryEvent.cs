using UnityEngine;

public struct RequestOpenInventoryEvent : IEvent
{
    public string EntityStableID;

    public RequestOpenInventoryEvent(string entityID)
    {
        EntityStableID = entityID;
    }
}
