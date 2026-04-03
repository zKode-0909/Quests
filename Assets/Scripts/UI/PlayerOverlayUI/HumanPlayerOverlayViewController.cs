using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class HumanPlayerOverlayViewController : VisualElement 
{

    StyleSheet styleSheet;

    VisualElement root;

    VisualElement humanPortraitHolder;
    VisualElement selectedPortraitHolder;
    VisualElement humanPlayerOverlayHolder;
    VisualElement partyPortraitHolder1;
    VisualElement partyPortraitHolder2;
    VisualElement partyPortraitHolder3;
    VisualElement partyPortraitHolder4;

    


    VisualElement partyPortraitsHolder;

    Portrait humanPortrait;
    Portrait selectedPortrait;

    Portrait partyPortrait1;
    Portrait partyPortrait2;
    Portrait partyPortrait3;
    Portrait partyPortrait4;

    PortraitManager portraits;
    PortraitContextMenu contextMenu;

  //  EventBinding<SelectionChangedEvent> selectionChangedEventBinding;
    EventBinding<PlayerLoadedEvent> playerLoadedEventBinding;
    EventBinding<PartyJoinedEvent> partyJoinedEventBinding;
    EventBinding<RemoveFromPartyEvent> removeFromPartyEventBinding;

    ISelectable overlayOwner;

    bool layoutBuilt = false;

    public HumanPlayerOverlayViewController(VisualElement root,StyleSheet styleSheet) {
        this.root = root;
        this.styleSheet = styleSheet;
        
    }

    public void Initialize() {
        Debug.Log("INITIALIZING OVERLAY");
        portraits = new PortraitManager();
        root.Clear();
        root.styleSheets.Add(styleSheet);
        root.name = "overlayRoot";
        root.AddToClassList("root");

       // selectionChangedEventBinding = new EventBinding<SelectionChangedEvent>(BuildSelectedPortrait);
        playerLoadedEventBinding = new EventBinding<PlayerLoadedEvent>(BuildLayout);
        partyJoinedEventBinding = new EventBinding<PartyJoinedEvent>(HandlePartyJoined);
        removeFromPartyEventBinding = new EventBinding<RemoveFromPartyEvent>(HandleRemovedFromParty);

       // EventBus<SelectionChangedEvent>.Register(selectionChangedEventBinding);
        EventBus<PlayerLoadedEvent>.Register(playerLoadedEventBinding);
        EventBus<PartyJoinedEvent>.Register(partyJoinedEventBinding);
        EventBus<RemoveFromPartyEvent>.Register(removeFromPartyEventBinding);
    }

    
    public void Dispose()
    {
        //EventBus<SelectionChangedEvent>.Deregister(selectionChangedEventBinding);
        EventBus<PlayerLoadedEvent>.Deregister(playerLoadedEventBinding);
        EventBus<PartyJoinedEvent>.Deregister(partyJoinedEventBinding);
        EventBus<RemoveFromPartyEvent>.Deregister(removeFromPartyEventBinding);

        if (humanPortrait != null)
        {
            humanPortrait.showContextMenuOnPortrait -= ShowContextBox;
            humanPortrait.Dispose();
        }

        if (selectedPortrait != null)
        {
            selectedPortrait.showContextMenuOnPortrait -= ShowContextBox;
            selectedPortrait.Dispose();
        }

        root.Clear();

    }

    void HandleRemovedFromParty(RemoveFromPartyEvent evt) {
        if (portraits.TryRemovePartyPortrait(evt.toBeRemoved, partyPortraitsHolder, out var removed)) {
            Debug.Log("Removed party portrait");
        }
    }
    void HandlePartyJoined(PartyJoinedEvent evt) {
        if (evt.OwnerEntityRuntimeID == overlayOwner.EntityRuntimeID) {
            Debug.Log("player has joined overlay owner's party");
            if (portraits.TryAddPartyPortrait(evt.Joiner, partyPortraitsHolder, out var portrait))
            {
                Debug.Log("Succesfully added portrait to partyPortraits");
                portrait.showContextMenuOnPortrait += ShowContextBox;  
                partyPortraitsHolder.style.display = DisplayStyle.Flex;

            }
            else {
                Debug.Log("Failed to add portrait");
            }
        }
    }

    void BuildLayout(PlayerLoadedEvent evt)
    {
        Debug.Log("Building Layout");
        overlayOwner = evt.playerStats.selectable;
        if (!layoutBuilt)
        {
            BuildUIPanel();
            CreateContextBox();
            root.pickingMode = PickingMode.Position;
            root.RegisterCallback<MouseDownEvent>(HandleMouseDown);
            root.RegisterCallback<MouseMoveEvent>(HandleMouseMove);

            layoutBuilt = true;
        }

        BuildHumanPlayerPortrait(evt.playerStats.selectable);
    }

    void BuildUIPanel()
    {
        humanPlayerOverlayHolder = new VisualElement();
        humanPlayerOverlayHolder.name = "overlayHolder";
        BuildPortraits();

        root.Add(humanPlayerOverlayHolder);
    }

    void BuildPortraits() {
        humanPortraitHolder = new VisualElement();
        selectedPortraitHolder = new VisualElement();

        partyPortraitsHolder = new VisualElement();

        partyPortraitHolder1 = new VisualElement();
        partyPortraitHolder2 = new VisualElement();
        partyPortraitHolder3 = new VisualElement();
        partyPortraitHolder4 = new VisualElement();

        partyPortraitsHolder.Add(partyPortraitHolder1);
        partyPortraitsHolder.Add(partyPortraitHolder2);
        partyPortraitsHolder.Add(partyPortraitHolder3);
        partyPortraitsHolder.Add(partyPortraitHolder4);
        


        humanPlayerOverlayHolder.AddToClassList("overlay");
        humanPortraitHolder.AddToClassList("humanPortraitHolder");
        selectedPortraitHolder.AddToClassList("selectedPortraitHolder");
        partyPortraitsHolder.AddToClassList("partyPortraitsHolder");

        selectedPortraitHolder.style.display = DisplayStyle.None;
        partyPortraitsHolder.style.display = DisplayStyle.None;    

        humanPlayerOverlayHolder.Add(humanPortraitHolder);
        humanPlayerOverlayHolder.Add(selectedPortraitHolder);
        humanPlayerOverlayHolder.Add(partyPortraitsHolder);
    }

    void CreateContextBox()
    {
        contextMenu = new PortraitContextMenu(overlayOwner);
        contextMenu.AddToClassList("contextBox");
        contextMenu.style.position = Position.Absolute;
        contextMenu.style.display = DisplayStyle.None;
        contextMenu.style.width = 160;
        contextMenu.style.height = 100;
        contextMenu.style.backgroundColor = Color.white;

        humanPlayerOverlayHolder.Add(contextMenu);
    }

    void BuildHumanPlayerPortrait(ISelectable selectable)
    {
        ClearHumanPortrait();

        if (selectable == null)
            return;

        if (portraits.TryAddPortrait(selectable, out var portrait))
        {
            humanPortrait = portrait;
            humanPortrait.showContextMenuOnPortrait += ShowContextBox;
            humanPortraitHolder.Add(humanPortrait);
        }
    }

    public ISelectable Select(Vector2 mousePosition)
    {

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Debug.Log("TRYING SELECT");

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<ISelectable>(out var selectable))
            {
                
                //Debug.Log($"I have hit the selectable {selected}");
                return selectable;
            }
        }

        
        return null;
    }


    void BuildSelectedPortrait(ISelectable selected,Vector2 mousePos)
    {

        //Debug.Log($"mousePos: {mousePos}, screen: {Screen.width}x{Screen.height}, panelScale: {humanPlayerOverlayHolder.panel.scale}");
        VisualElement picked = humanPlayerOverlayHolder.panel.Pick(mousePos);
        Portrait clickedPortrait = GetTypeFromPickedElement<Portrait>(picked);
        PortraitContextMenu menu = GetTypeFromPickedElement<PortraitContextMenu>(picked);

        Debug.Log($"picked: {picked}, clickedPortrait: {clickedPortrait}, menu: {menu}");

        if (menu != null)
        {
            // clicked context menu → handle that first
            Debug.Log("hit menu");
        }
        else if (clickedPortrait != null && humanPortrait != null && clickedPortrait.id != humanPortrait.id)
        {
            Debug.Log($"Showing clicked portrait");
            HideContextBox();
            if (selectedPortrait != null && selectedPortrait.id == clickedPortrait.id)
                return;

            Debug.Log("selected portrait is null");
            
            ClearSelectedPortrait();
            if (portraits.TryAddPortrait(clickedPortrait.GetDisplayedPortrait(), out var newSelection)) {
                selectedPortrait = newSelection;
                selectedPortrait.showContextMenuOnPortrait += ShowContextBox;
                selectedPortraitHolder.Add(selectedPortrait);
                selectedPortraitHolder.style.display = DisplayStyle.Flex;
            }
            
            return;
        }
        else
        {
            Debug.Log("in selected elsewhere");
            // clicked elsewhere
            // Clicked in the world and selected something
            if (selected != null)
            {
                Debug.Log("selected is not null");
                if (portraits.TryAddPortrait(selected, out var portrait))
                {
                    Debug.Log("Added portrait");
                    ClearSelectedPortrait();
                    selectedPortrait = portrait;
                    selectedPortrait.showContextMenuOnPortrait += ShowContextBox;
                    selectedPortraitHolder.Add(selectedPortrait);
                    selectedPortraitHolder.style.display = DisplayStyle.Flex;
                }

                return;
            }
            Debug.Log("CLIcked nothing useful");
            // Clicked nothing useful
            HideContextBox();
            ClearSelectedPortrait();
        }
 
    }

    T GetTypeFromPickedElement<T>(VisualElement elt) where T : VisualElement
    {
        while (elt != null)
        {
            if (elt is T targetType)
                return targetType;

            elt = elt.parent;
        }

        return null;
    }



    void ClearHumanPortrait()
    {
        if (humanPortrait == null)
            return;

        humanPortrait.showContextMenuOnPortrait -= ShowContextBox;
        humanPortrait.Dispose();

        portraits.TryRemovePortrait(humanPortrait.GetDisplayedPortrait(), out var _);

        humanPortraitHolder.Clear();
        humanPortrait = null;
    }

    void ClearSelectedPortrait()
    {
        Debug.Log("Clear selected portrait");
        if (selectedPortrait == null)
            return;

        Debug.Log("Clearing selected portrait");

        selectedPortrait.showContextMenuOnPortrait -= ShowContextBox;
        selectedPortrait.Dispose();

        portraits.TryRemovePortrait(selectedPortrait.GetDisplayedPortrait(), out var _);

        selectedPortraitHolder.Clear();
        selectedPortrait = null;
        selectedPortraitHolder.style.display = DisplayStyle.None;
    }

    void ShowContextBox(Vector2 mousePos,ISelectable selected)
    {
        contextMenu.style.left = mousePos.x;
        contextMenu.style.top = mousePos.y;
        contextMenu.style.display = DisplayStyle.Flex;
        var selectedData = selected.SendSelectionData();

        if (selectedData.isHuman)
        {
            contextMenu.BuildHumanPlayerContextMenu(selected);
        }
        else if (selectedData.isPlayer && !selectedData.isHuman) {
            contextMenu.BuildSimPlayerContextMenu(selected);
        }
    }

    void HideContextBox()
    {
        if (contextMenu != null)
        {
            contextMenu.style.display = DisplayStyle.None;
        }
    }


    void HandleMouseDown(MouseDownEvent evt)
    {
        Vector2 uiPos = evt.mousePosition;

        /*
        Vector2 worldPos = evt.mousePosition;
        worldPos.y = Screen.height - worldPos.y;
        */
        


        BuildSelectedPortrait(Select(Mouse.current.position.ReadValue()), uiPos);
    }

    void HandleMouseMove(MouseMoveEvent evt) {
        var objectHovered = Select(Mouse.current.position.ReadValue());
        Debug.Log("Handling mouse move event");
        if (objectHovered != null) {
            Debug.Log($"Hovering selectable named: {objectHovered.SendSelectionData().selectedName}");
        }
    }

   


    
}