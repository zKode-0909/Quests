using UnityEngine;

public class InventoryBootstrapper : MonoBehaviour
{
    InventoryController inventoryController;
    InventoryRegistry inventoryRegistry;
    ItemFactory itemFactory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Bootstrap(InventoryRegistry registry,ItemFactory factory) {
        itemFactory = factory;
        inventoryController = new InventoryController();
        inventoryController.InitiateService(registry,itemFactory);
    }
}
