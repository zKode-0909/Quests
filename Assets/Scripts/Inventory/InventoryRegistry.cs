using System.Collections.Generic;
using UnityEngine;

public sealed class InventoryRegistry
{
    EventBinding<RegisterInventoryEvent> registerInventoryBinding;
   


    private readonly Dictionary<string, Inventory> _inventories = new();

    public IEnumerable<KeyValuePair<string,Inventory>> All => _inventories;

    public bool TryGet(string id, out Inventory inventory) => _inventories.TryGetValue(id, out inventory);

    public void InitializeRegistry() {
        registerInventoryBinding = new EventBinding<RegisterInventoryEvent>(RegisterInventory);
        EventBus<RegisterInventoryEvent>.Register(registerInventoryBinding);
    }


    public void RegisterInventory(RegisterInventoryEvent evt) {
        Debug.Log($"registering inventory for {evt.EntityStableID}");
        GetOrCreate(evt.EntityStableID);
    }

    public bool RegisterInventory(string ownerID,Inventory inventory) {
        Debug.Log($"trying to register inventory for {ownerID} with inventory {inventory}");
        return _inventories.TryAdd(ownerID, inventory);
    }

    public Inventory GetOrCreate(string id)
    {
        if (_inventories.TryGetValue(id, out var inventory)) return inventory;
        inventory = new Inventory(50,id);
        _inventories.Add(id, inventory);
        return inventory;
    }

    public bool Remove(string id) => _inventories.Remove(id);

    public bool TryCreate(string id)
    {
        if (_inventories.TryGetValue(id, out var inventory))
        {
            return false;
        }

        inventory = new Inventory(50, id);
        _inventories.Add(id, inventory);
        Debug.Log($"created inventory for id {id}");
        return true;

    }
}
