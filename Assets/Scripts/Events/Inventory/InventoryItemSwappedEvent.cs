using UnityEngine;

public readonly struct InventoryItemSwappedEvent : IEvent
{
    public readonly int item1Idx;
    public readonly int item2Idx;
    public readonly int EntityRuntimeID;

    public InventoryItemSwappedEvent(int idx1, int idx2,int id)
    {
        this.item1Idx = idx1;
        this.item2Idx = idx2;
        this.EntityRuntimeID = id;
    }
}
