

using System.Collections.Generic;
using UnityEngine;

public class InventoryController
{
    private EventBinding<RequestOpenInventoryEvent> displayInventoryReqBinding;
    private EventBinding<RequestCloseInventoryEvent> closeInventoryReqBinding;

    private EventBinding<GatherItemEvent> gatherItemReqBinding;
    private EventBinding<InventoryItemSwappedEvent> swappedItemsBinding;

    InventoryRegistry inventoryRegistry;
    ItemFactory itemFactory;


   

    public void InitiateService(InventoryRegistry registry,ItemFactory factory)
    {

        displayInventoryReqBinding = new EventBinding<RequestOpenInventoryEvent>(OnOpenInventoryRequested);
        closeInventoryReqBinding = new EventBinding<RequestCloseInventoryEvent>(OnCloseInventoryRequested);

        gatherItemReqBinding = new EventBinding<GatherItemEvent>(OnAddItemToInventory);

        swappedItemsBinding = new EventBinding<InventoryItemSwappedEvent>(HandleItemSwap);

        inventoryRegistry = registry;
        itemFactory = factory;
   


        EventBus<RequestOpenInventoryEvent>.Register(displayInventoryReqBinding);
        EventBus<RequestCloseInventoryEvent>.Register(closeInventoryReqBinding);
        EventBus<GatherItemEvent>.Register(gatherItemReqBinding);
        EventBus<InventoryItemSwappedEvent>.Register(swappedItemsBinding);
    }


    public void Dispose()
    {
        EventBus<RequestOpenInventoryEvent>.Deregister(displayInventoryReqBinding);
        EventBus<RequestCloseInventoryEvent>.Deregister(closeInventoryReqBinding);
        EventBus<GatherItemEvent>.Deregister(gatherItemReqBinding);
        EventBus<InventoryItemSwappedEvent>.Deregister(swappedItemsBinding);


    }

    void OnAddItemToInventory(GatherItemEvent evt) {
        if (inventoryRegistry.TryGet(evt.gatheredByStableID, out var inventory)) {
            if (itemFactory.TryCreateItemFromID(evt.itemStableID, out var item)) {
                if (inventory.TryAddItemToInventory(item)) {
                    Debug.Log($"{item.ItemName} has been added to {evt.gatheredByStableID}'s inventory");
                }
            }
        }
    }

    void HandleItemSwap(InventoryItemSwappedEvent evt) {
        if (inventoryRegistry.TryGet(evt.EntityStableID, out var inventory)) {
            inventory.SwapItems(evt.item1Idx, evt.item2Idx);
        }

    }


    void OnOpenInventoryRequested(RequestOpenInventoryEvent evt) {
   
        Debug.Log($"Trying to open Inventory for {evt.EntityStableID}");
        var displayList = new List<InventoryUIItem>();
       
        if (inventoryRegistry.TryGet(evt.EntityStableID, out var inventory))
        {
            Debug.Log("opening inventory");
            EventBus<DisplayInventoryEvent>.Raise(new DisplayInventoryEvent(BuildUIListFromInventory(inventory),evt.EntityStableID));
        }
        else
        {
            Debug.LogWarning("failed opening quest log");
        }
    }

    void OnCloseInventoryRequested(RequestCloseInventoryEvent evt) {
        EventBus<CloseInventoryEvent>.Raise(new CloseInventoryEvent());
    }

    List<InventoryUIItem> BuildUIListFromInventory(Inventory inventory) {
        var uiInventory = new List<InventoryUIItem>();

        foreach (var item in inventory.GetItems()) {
            if (item != null)
            {
                var uiInventoryItem = new InventoryUIItem(item.ItemName, item.Icon);
                uiInventory.Add(uiInventoryItem);
            }
            else {
                var uiInventoryItem = new InventoryUIItem("blank", null);
                uiInventory.Add(uiInventoryItem);
            }
            
        }

        return uiInventory;
    }
}
