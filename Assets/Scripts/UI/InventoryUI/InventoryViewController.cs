using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryViewController : MonoBehaviour
{
    [SerializeField] UIDocument document;
    [SerializeField] StyleSheet styleSheet;
    [SerializeField] Sprite placeholderSprite;

    VisualElement root;

    EventBinding<DisplayInventoryEvent> displayInventory;
    EventBinding<CloseInventoryEvent> closeInventory;

    VisualElement inventoryContainer;
    VisualElement inventoryGrid;
    VisualElement inventoryItemHeld;
    VisualElement clickRegister;

    List<InventoryItemVisualElement> ItemsBeingDisplayed;
    List<InventorySlot> inventorySlots;

    InventorySlot selectedSlot;

    int entityID;

    bool itemSelected = false;

    void Start()
    {
        if (document == null) return;

        root = document.rootVisualElement;
        if (root == null) return;

        root.Clear();
        root.styleSheets.Add(styleSheet);
        root.AddToClassList("inventoryDisplay");

        displayInventory = new EventBinding<DisplayInventoryEvent>(OpenInventory);
        EventBus<DisplayInventoryEvent>.Register(displayInventory);

        closeInventory = new EventBinding<CloseInventoryEvent>(CloseInventory);
        EventBus<CloseInventoryEvent>.Register(closeInventory);

        BuildInventory();
        root.pickingMode = PickingMode.Ignore;
        root.style.display = DisplayStyle.None;
        root.RegisterCallback<PointerMoveEvent>(OnPointerMove);
    }

    private void OnDisable()
    {
        if (displayInventory != null)
            EventBus<DisplayInventoryEvent>.Deregister(displayInventory);

        if (closeInventory != null)
            EventBus<CloseInventoryEvent>.Deregister(closeInventory);

        if (root != null)
            root.UnregisterCallback<PointerMoveEvent>(OnPointerMove);
    }

    private void OnDestroy()
    {
        if (root != null)
            root.UnregisterCallback<PointerMoveEvent>(OnPointerMove);

        if (inventorySlots != null)
        {
            foreach (var slot in inventorySlots)
            {
                if (slot == null) continue;
                slot.ClickedSlotEvent -= HandleClickItem;
                slot.Dispose();
            }
        }
    }

    InventorySlot hoveredSlot;

    private void OnPointerMove(PointerMoveEvent evt)
    {
        if (!itemSelected || root?.panel == null) return;

        Vector2 local = root.WorldToLocal(evt.position);
        inventoryItemHeld.style.left = local.x - 32;
        inventoryItemHeld.style.top = local.y - 32;

        var picked = root.panel.Pick(evt.position);
        hoveredSlot = FindAncestorSlot(picked);
    }

    private InventorySlot FindAncestorSlot(VisualElement ve)
    {
        while (ve != null)
        {
            if (ve is InventorySlot slot)
                return slot;

            ve = ve.parent;
        }
        return null;
    }

    void BuildInventory()
    {
        ItemsBeingDisplayed = new List<InventoryItemVisualElement>();
        inventorySlots = new List<InventorySlot>();

        inventoryItemHeld = new VisualElement();
        inventoryItemHeld.style.position = Position.Absolute;
        inventoryItemHeld.style.width = 64;
        inventoryItemHeld.style.height = 64;
        inventoryItemHeld.style.display = DisplayStyle.None;
        inventoryItemHeld.style.flexGrow = 0;
        inventoryItemHeld.style.flexShrink = 0;
        inventoryItemHeld.pickingMode = PickingMode.Ignore;

        inventoryContainer = new VisualElement();
        inventoryContainer.AddToClassList("inventoryContainer");

        inventoryGrid = new VisualElement();
        inventoryGrid.AddToClassList("inventoryGrid");

        inventoryGrid.pickingMode = PickingMode.Ignore;
        inventoryContainer.pickingMode = PickingMode.Ignore;

        inventoryContainer.Add(inventoryGrid);



        root.Add(inventoryContainer);
        root.Add(inventoryItemHeld);
    }

    void OpenInventory(DisplayInventoryEvent evt)
    {
        if (root == null || inventoryGrid == null) return;

        entityID = evt.EntityRuntimeID;
        Debug.Log("opening inventory in view");
        int idx = 0;
        foreach (var item in evt.Items)
        {
            var inventorySlot = new InventorySlot(placeholderSprite,idx);
            inventorySlot.ClickedSlotEvent += HandleClickItem;

            inventorySlots.Add(inventorySlot);
            if (item.sprite == null) {
                inventorySlot.SetInventoryItem(null);
            }
            else{
                var itemElt = new InventoryItemVisualElement(item.title, item.sprite);

                inventorySlot.SetInventoryItem(itemElt);
            }

            idx++;
            inventoryGrid.Add(inventorySlot);
        }

        root.style.display = DisplayStyle.Flex;
    }

    void HandleClickItem(InventorySlot itemClicked)
    {

        Debug.Log($"clicked {itemClicked.GetSlotIdx()}");
        if (!itemSelected)
        {
            Debug.Log("I hate UI");
            if (itemClicked.GetInventoryItem() == null) return;
            Debug.Log("Clicked Item");
            inventoryItemHeld.style.backgroundImage = new StyleBackground(itemClicked.GetInventoryItem().GetItemSprite());
            inventoryItemHeld.style.display = DisplayStyle.Flex;
            // root.CapturePointer(PointerId.mousePointerId);
            selectedSlot = itemClicked;
            itemSelected = true;
        }
        else
        {
            if (itemClicked == null)
            {
                inventoryItemHeld.style.backgroundImage = new StyleBackground();
            }
            else { 
                var tempItem = itemClicked.GetInventoryItem();
                itemClicked.SetInventoryItem(selectedSlot.GetInventoryItem());
                //Debug.Log($"placed {selectedSlot.GetInventoryItem()} in index {itemClicked.GetSlotIdx()}");
                selectedSlot.SetInventoryItem(tempItem);
                //Debug.Log($"placed {tempItem} in index {selectedSlot.GetSlotIdx()}");
                inventoryItemHeld.style.backgroundImage = new StyleBackground();

                EventBus<InventoryItemSwappedEvent>.Raise(new InventoryItemSwappedEvent(selectedSlot.GetSlotIdx(), itemClicked.GetSlotIdx(),entityID));
            }
            //Debug.Log("placing item");
            itemSelected = false;

            // if (root.HasPointerCapture(PointerId.mousePointerId)) {
            // root.ReleasePointer(PointerId.mousePointerId);
            //     Debug.Log("released pointer");

            //   }




        }
    }

    void CloseInventory()
    {
        if (root == null) return;

        root.style.display = DisplayStyle.None;

        foreach (var slot in inventorySlots)
        {
            if (slot == null) continue;
            slot.ClickedSlotEvent -= HandleClickItem;
            slot.Dispose();
        }

        inventoryGrid.Clear();
        inventorySlots.Clear();
        itemSelected = false;
        inventoryItemHeld.style.display = DisplayStyle.None;
    }
}