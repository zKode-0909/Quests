using System.Collections.Generic;

public struct DisplayInventoryEvent : IEvent
{
    public IReadOnlyList<InventoryUIItem> Items;
    public int EntityRuntimeID;

    public DisplayInventoryEvent(IReadOnlyList<InventoryUIItem> items,int id)
    {
        Items = items;
        EntityRuntimeID = id;
    }
}
