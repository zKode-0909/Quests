using UnityEngine;
using UnityEngine.UIElements;

public class InventoryViewController : MonoBehaviour
{
    [SerializeField] UIDocument document;
    [SerializeField] StyleSheet styleSheet;
    VisualElement root;

    EventBinding<DisplayInventoryEvent> displayInventory;
    EventBinding<CloseInventoryEvent> closeInventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        root = document.rootVisualElement;
        root.Clear();

        root.styleSheets.Add(styleSheet);

        root.AddToClassList("inventoryDisplay");

        displayInventory = new EventBinding<DisplayInventoryEvent>(OpenInventory);
        EventBus<DisplayInventoryEvent>.Register(displayInventory);

        closeInventory = new EventBinding<CloseInventoryEvent>(CloseInventory);
        EventBus<CloseInventoryEvent>.Register(closeInventory);

        BuildInventory();


        root.style.display = DisplayStyle.None;
    }

    void BuildInventory() {
        var inventoryContainer = new VisualElement();
        inventoryContainer.AddToClassList("inventoryContainer");
        var inventoryGrid = new VisualElement();
        inventoryGrid.AddToClassList("inventoryGrid");

        inventoryContainer.Add(inventoryGrid);
        root.Add(inventoryContainer);
    }


    void OpenInventory() {
        Debug.Log("opening inventory in view");
        root.style.display = DisplayStyle.Flex;
    }

    void CloseInventory() {
        root.style.display = DisplayStyle.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
