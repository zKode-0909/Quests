using System.Collections.Generic;
using UnityEngine;

public sealed class InventoryRegistry
{
    EventBinding<RegisterInventoryEvent> registerInventoryBinding;
   


    private readonly Dictionary<int, Inventory> _inventories = new();

    public IEnumerable<KeyValuePair<int,Inventory>> All => _inventories;

    public bool TryGet(int id, out Inventory inventory) => _inventories.TryGetValue(id, out inventory);

    public void InitializeRegistry() {
        registerInventoryBinding = new EventBinding<RegisterInventoryEvent>(RegisterInventory);
        EventBus<RegisterInventoryEvent>.Register(registerInventoryBinding);
    }


    public void RegisterInventory(RegisterInventoryEvent evt) {
        Debug.Log($"registering inventory for {evt.EntityRuntimeID}");
        GetOrCreate(evt.EntityRuntimeID);
    }

    public Inventory GetOrCreate(int id)
    {
        if (_inventories.TryGetValue(id, out var inventory)) return inventory;
        inventory = new Inventory(50);
        _inventories.Add(id, inventory);
        return inventory;
    }

    public bool Remove(int id) => _inventories.Remove(id);

    public bool TryCreate(int id)
    {
        if (_inventories.TryGetValue(id, out var inventory))
        {
            return false;
        }

        inventory = new Inventory(50);
        _inventories.Add(id, inventory);
        Debug.Log($"created inventory for id {id}");
        return true;

    }
}
