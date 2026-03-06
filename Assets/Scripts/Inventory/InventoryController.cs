using Codice.Client.BaseCommands.CheckIn;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController
{
    private EventBinding<RequestOpenInventoryEvent> displayInventoryReqBinding;
    private EventBinding<RequestCloseInventoryEvent> closeInventoryReqBinding;

    private EventBinding<GatherItemEvent> gatherItemReqBinding;

    InventoryRegistry inventoryRegistry;
    ItemFactory itemFactory;


   

    public void InitiateService(InventoryRegistry registry,ItemFactory factory)
    {

        displayInventoryReqBinding = new EventBinding<RequestOpenInventoryEvent>(OnOpenInventoryRequested);
        closeInventoryReqBinding = new EventBinding<RequestCloseInventoryEvent>(OnCloseInventoryRequested);

        gatherItemReqBinding = new EventBinding<GatherItemEvent>(OnAddItemToInventory);

        inventoryRegistry = registry;
        itemFactory = factory;
   


        EventBus<RequestOpenInventoryEvent>.Register(displayInventoryReqBinding);
        EventBus<RequestCloseInventoryEvent>.Register(closeInventoryReqBinding);
        EventBus<GatherItemEvent>.Register(gatherItemReqBinding);
    }


    public void Dispose()
    {
        EventBus<RequestOpenInventoryEvent>.Deregister(displayInventoryReqBinding);
        EventBus<RequestCloseInventoryEvent>.Deregister(closeInventoryReqBinding);
        EventBus<GatherItemEvent>.Deregister(gatherItemReqBinding);


    }

    void OnAddItemToInventory(GatherItemEvent evt) {
        if (inventoryRegistry.TryGet(evt.gatheredByRuntimeID, out var inventory)) {
            if (itemFactory.TryCreateItemFromID(evt.itemStableID, evt.gatheredByRuntimeID, out var item)) { 
                inventory.TryAddItemToInventory(item);
            }
        }
    }


    void OnOpenInventoryRequested(RequestOpenInventoryEvent evt) {
   
        Debug.Log($"Trying to open Inventory for {evt.EntityRuntimeID}");
        var displayList = new List<InventoryUIItem>();
       
        if (inventoryRegistry.TryGet(evt.EntityRuntimeID, out var inventory))
        {
            Debug.Log("opening inventory");
            EventBus<DisplayInventoryEvent>.Raise(new DisplayInventoryEvent(displayList));
        }
        else
        {
            Debug.LogWarning("failed opening quest log");
        }
    }

    void OnCloseInventoryRequested(RequestCloseInventoryEvent evt) {
        EventBus<CloseInventoryEvent>.Raise(new CloseInventoryEvent());
    }
}
