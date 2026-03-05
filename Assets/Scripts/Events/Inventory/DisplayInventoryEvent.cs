using System.Collections.Generic;

public struct DisplayInventoryEvent : IEvent
{
    public IReadOnlyList<InventoryUIItem> Items;

    public DisplayInventoryEvent(IReadOnlyList<InventoryUIItem> items)
    {
        Items = items;
    }
}
