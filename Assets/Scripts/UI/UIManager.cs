using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{

    [SerializeField] UIDocument uiDocument;

    [SerializeField] StyleSheet rootStyle;
    [SerializeField] StyleSheet overlayStyle;
    [SerializeField] StyleSheet questlogStyle;
    [SerializeField] StyleSheet questhandoutStyle;
    [SerializeField] StyleSheet inventoryStyle;
    [SerializeField] StyleSheet menuStyle;

    [SerializeField] Sprite inventoryPlaceholderSprite;

    HumanPlayerOverlayViewController overlayController;
    QuestHandoutView questGiverHandoutView;
    QuestLogView questLogView;
    InventoryViewController inventoryView;
    InGameMenu inGameMenu;

    EventBinding<OpenQuestGiverUI> openBinding;

    EventBinding<DisplayQuestLogEvent> displayQuestLog;
    EventBinding<CloseQuestLogEvent> closeQuestLog;

    EventBinding<DisplayInventoryEvent> displayInventory;
    EventBinding<CloseInventoryEvent> closeInventory;

    EventBinding<ShowMenuEvent> showMenu;
    EventBinding<HideMenuEvent> hideMenu;

    private VisualElement root;

    private VisualElement overlayRoot;
    private VisualElement questGiverRoot;
    private VisualElement questLogRoot;
    private VisualElement inventoryRoot;
    private VisualElement menuRoot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        
        var root = GetComponent<UIDocument>().rootVisualElement;
        root.Clear();
        root.styleSheets.Add(rootStyle);

        overlayRoot = new VisualElement();
        questGiverRoot = new VisualElement();
        questLogRoot = new VisualElement();
        inventoryRoot = new VisualElement();
        menuRoot = new VisualElement();

        root.AddToClassList("uiRoot");
        

        root.Add(overlayRoot);
        root.Add(questGiverRoot);
        root.Add(questLogRoot);
        root.Add(inventoryRoot);
        root.Add(menuRoot);

        overlayController = new HumanPlayerOverlayViewController(overlayRoot, overlayStyle);
        overlayController.Initialize();

        questGiverHandoutView = new QuestHandoutView(questGiverRoot, questhandoutStyle);
        questGiverHandoutView.Initialize();

        questLogView = new QuestLogView(questLogRoot, questlogStyle);
        questLogView.Initialize();

        inventoryView = new InventoryViewController(inventoryRoot, inventoryStyle, inventoryPlaceholderSprite);
        inventoryView.Initialize();

        openBinding = new EventBinding<OpenQuestGiverUI>(questGiverHandoutView.HandleOpenUI);
        EventBus<OpenQuestGiverUI>.Register(openBinding);

        displayQuestLog = new EventBinding<DisplayQuestLogEvent>(questLogView.OpenQuestLog);
        EventBus<DisplayQuestLogEvent>.Register(displayQuestLog);

        closeQuestLog = new EventBinding<CloseQuestLogEvent>(questLogView.CloseQuestLog);
        EventBus<CloseQuestLogEvent>.Register(closeQuestLog);

        displayInventory = new EventBinding<DisplayInventoryEvent>(inventoryView.OpenInventory);
        EventBus<DisplayInventoryEvent>.Register(displayInventory);

        closeInventory = new EventBinding<CloseInventoryEvent>(inventoryView.CloseInventory);
        EventBus<CloseInventoryEvent>.Register(closeInventory);

        inGameMenu = new InGameMenu(menuRoot, menuStyle);
        inGameMenu.Initialize();


        showMenu = new EventBinding<ShowMenuEvent>(inGameMenu.ShowMenu);
        EventBus<ShowMenuEvent>.Register(showMenu);

        hideMenu = new EventBinding<HideMenuEvent>(inGameMenu.HideMenu);
        EventBus<HideMenuEvent>.Register(hideMenu);

        
    }

    private void OnDisable()
    {
        overlayController.Dispose();
        questGiverHandoutView.Dispose();
        questLogView.Dispose();
        inventoryView.Dispose();

        EventBus<OpenQuestGiverUI>.Deregister(openBinding);

        EventBus<DisplayQuestLogEvent>.Deregister(displayQuestLog);
        EventBus<CloseQuestLogEvent>.Deregister(closeQuestLog);

        EventBus<DisplayInventoryEvent>.Deregister(displayInventory);
        EventBus<CloseInventoryEvent>.Deregister(closeInventory);

        EventBus<ShowMenuEvent>.Deregister(showMenu);
        EventBus<HideMenuEvent>.Deregister(hideMenu);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
