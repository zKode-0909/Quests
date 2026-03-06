using UnityEngine;

public struct AddItemToInventoryEvent : IEvent
{
    public int EntityID;
    public string ItemStableID;

    public AddItemToInventoryEvent(int entityID, string itemID) { 
        EntityID = entityID;
        ItemStableID = itemID;
    }
}