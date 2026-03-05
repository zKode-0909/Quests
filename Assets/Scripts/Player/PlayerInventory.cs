using UnityEngine;

public class PlayerInventory
{
    private bool isOpen;

    public void OpenInventory(int id) {
        if (!isOpen)
        {
            EventBus<RequestOpenInventoryEvent>.Raise(new RequestOpenInventoryEvent(id));
        }
        else {
            EventBus<RequestCloseInventoryEvent>.Raise(new RequestCloseInventoryEvent(id));
        }
    }
}
