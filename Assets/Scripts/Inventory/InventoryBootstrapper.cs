using UnityEngine;

public class InventoryBootstrapper : MonoBehaviour
{
    InventoryController inventoryController;
    InventoryRegistry inventoryRegistry;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Bootstrap(InventoryRegistry registry) {
        inventoryController = new InventoryController();
        inventoryController.InitiateService(registry);
    }
}
