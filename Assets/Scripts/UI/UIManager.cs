using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{

    [SerializeField] UIDocument uiDocument;

    [SerializeField] StyleSheet overlayStyle;
    [SerializeField] StyleSheet questlogStyle;
    [SerializeField] StyleSheet questhandoutStyle;
    [SerializeField] StyleSheet inventoryStyle;

    [SerializeField] Sprite inventoryPlaceholderSprite;

    HumanPlayerOverlayViewController overlayController;
    QuestHandoutView questGiverHandoutView;
    QuestLogView questLogView;
    InventoryViewController inventoryView;

    EventBinding<OpenQuestGiverUI> openBinding;

    EventBinding<DisplayQuestLogEvent> displayQuestLog;
    EventBinding<CloseQuestLogEvent> closeQuestLog;

    EventBinding<DisplayInventoryEvent> displayInventory;
    EventBinding<CloseInventoryEvent> closeInventory;

    private VisualElement root;

    private VisualElement overlayRoot;
    private VisualElement questGiverRoot;
    private VisualElement questLogRoot;
    private VisualElement inventoryRoot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        overlayRoot = new VisualElement();
        questGiverRoot = new VisualElement();
        questLogRoot = new VisualElement();
        inventoryRoot = new VisualElement();

        overlayController = new HumanPlayerOverlayViewController(overlayRoot, overlayStyle);
        questGiverHandoutView = new QuestHandoutView(questGiverRoot, questhandoutStyle);
        questLogView = new QuestLogView(questLogRoot, questlogStyle);
        inventoryView = new InventoryViewController(inventoryRoot, inventoryStyle, inventoryPlaceholderSprite);

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
    }

    private void OnDisable()
    {
        overlayController.Dispose();
        inventoryView.Dispose();

        EventBus<OpenQuestGiverUI>.Deregister(openBinding);

        EventBus<DisplayQuestLogEvent>.Deregister(displayQuestLog);
        EventBus<CloseQuestLogEvent>.Deregister(closeQuestLog);

        EventBus<DisplayInventoryEvent>.Deregister(displayInventory);
        EventBus<CloseInventoryEvent>.Deregister(closeInventory);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
