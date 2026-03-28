using UnityEngine;

public readonly struct InventoryItemSwappedEvent : IEvent
{
    public readonly int item1Idx;
    public readonly int item2Idx;
    public readonly string EntityStableID;

    public InventoryItemSwappedEvent(int idx1, int idx2,string id)
    {
        this.item1Idx = idx1;
        this.item2Idx = idx2;
        this.EntityStableID = id;
    }
}
