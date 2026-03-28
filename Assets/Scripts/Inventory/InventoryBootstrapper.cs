using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBootstrapper : MonoBehaviour
{
    InventoryController inventoryController;
    InventoryRegistry inventoryRegistry;
    ItemFactory itemFactory;
    InventoryFactory inventoryFactory;

    List<InventorySaveData> data;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Bootstrap(InventoryRegistry registry,ItemFactory factory,
        InventoryFactory inventoryFactory,List<InventorySaveData> inventoryData) {
        this.inventoryFactory = inventoryFactory;
        this.inventoryRegistry = registry;
        itemFactory = factory;
        data = inventoryData;

        BuildInventoriesFromData();

        inventoryController = new InventoryController();
        inventoryController.InitiateService(registry,itemFactory);

    }

    void BuildInventoriesFromData() {
        foreach (var inventory in data) { 
            var builtInventory = inventoryFactory.BuildInventoryFromData(inventory);
            if (!inventoryRegistry.RegisterInventory(inventory.Owner, builtInventory)) {
                Debug.Log($"Failed to register inventory for {inventory.Owner}");
            }
        }
    }
}
