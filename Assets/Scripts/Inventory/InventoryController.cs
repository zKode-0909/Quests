using Codice.Client.BaseCommands.CheckIn;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController
{
    private EventBinding<RequestOpenInventoryEvent> displayInventoryReqBinding;
    private EventBinding<RequestCloseInventoryEvent> closeInventoryReqBinding;

    InventoryRegistry inventoryRegistry;


   

    public void InitiateService(InventoryRegistry registry)
    {
        displayInventoryReqBinding = new EventBinding<RequestOpenInventoryEvent>(OnOpenInventoryRequested);
        closeInventoryReqBinding = new EventBinding<RequestCloseInventoryEvent>(OnCloseInventoryRequested);
        inventoryRegistry = registry;
   


        EventBus<RequestOpenInventoryEvent>.Register(displayInventoryReqBinding);
        EventBus<RequestCloseInventoryEvent>.Register(closeInventoryReqBinding);
    }


    public void Dispose()
    {
        EventBus<RequestOpenInventoryEvent>.Deregister(displayInventoryReqBinding);
        EventBus<RequestCloseInventoryEvent>.Deregister(closeInventoryReqBinding);


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
