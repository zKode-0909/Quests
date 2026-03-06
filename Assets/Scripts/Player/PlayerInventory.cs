using UnityEngine;

public class PlayerInventory
{
    private bool isOpen;

    public void ToggleInventory(int id) {
        if (!isOpen)
        {
            isOpen = true;
            Debug.Log($"player inventory opening");
            EventBus<RequestOpenInventoryEvent>.Raise(new RequestOpenInventoryEvent(id));
        }
        else {
            isOpen = false;
            Debug.Log($"player inventory closing");
            EventBus<RequestCloseInventoryEvent>.Raise(new RequestCloseInventoryEvent(id));
        }
    }
}
