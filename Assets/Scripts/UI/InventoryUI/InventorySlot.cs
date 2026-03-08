using System;
using UnityEngine.UIElements;
using UnityEngine;

public class InventorySlot : VisualElement
{
    InventoryItemVisualElement inventoryItem;
    public event Action<InventorySlot> ClickedSlotEvent;
    int slotIdx;

    public InventorySlot(Sprite backgroundSprite, int slotIdx)
    {
        AddToClassList("inventorySlot");
        style.aspectRatio = 1;
        style.backgroundImage = new StyleBackground(backgroundSprite);

        RegisterCallback<PointerDownEvent>(OnPointerDown);
        this.slotIdx = slotIdx;
    }

    public int GetSlotIdx() { 
        return slotIdx;
    }

    private void OnPointerDown(PointerDownEvent evt)
    {
        ClickedSlotEvent?.Invoke(this);
    }

    public void Dispose()
    {
        UnregisterCallback<PointerDownEvent>(OnPointerDown);
    }

    public void SetInventoryItem(InventoryItemVisualElement itemVisualElement)
    {
        if (inventoryItem != null && inventoryItem.parent == this)
        {
            Remove(inventoryItem);
        }

        inventoryItem = itemVisualElement;

        if (inventoryItem != null)
        {
            Add(inventoryItem);
        }
    }

    public InventoryItemVisualElement GetInventoryItem()
    {
        return inventoryItem;
    }
}