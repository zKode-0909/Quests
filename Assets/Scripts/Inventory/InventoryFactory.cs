using UnityEngine;

public class InventoryFactory
{
    ItemFactory itemFactory;

    public InventoryFactory(ItemFactory itemFactory) { 
        this.itemFactory = itemFactory;
    }

    public Inventory BuildInventoryFromData(InventorySaveData data)
    {
        var inventory = new Inventory(data.Capacity, data.Owner);

        for (int i = 0; i < data.Items.Length; i++)
        {
            var itemId = data.Items[i];

            if (itemId != null && itemFactory.TryCreateItemFromID(itemId, out var runtimeItem))
            {
                inventory.SetItemAtIndex(i, runtimeItem);
            }
            else
            {
                inventory.SetItemAtIndex(i, null);
            }
        }

        return inventory;
    }
}
