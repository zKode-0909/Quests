using System.Collections.Generic;

public struct DisplayInventoryEvent : IEvent
{
    public IReadOnlyList<InventoryUIItem> Items;
    public string EntityStableID;

    public DisplayInventoryEvent(IReadOnlyList<InventoryUIItem> items,string id)
    {
        Items = items;
        EntityStableID = id;
    }
}
