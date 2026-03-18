using UnityEngine;
using UnityEngine.UIElements;

public class HumanPlayerOverlayViewController : MonoBehaviour
{
    [SerializeField] UIDocument document;
    [SerializeField] StyleSheet styleSheet;

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

    EventBinding<SelectionChangedEvent> selectionChangedEventBinding;
    EventBinding<PlayerLoadedEvent> playerLoadedEventBinding;

    ISelectable overlayOwner;

    bool layoutBuilt = false;

    private void Awake()
    {
        portraits = new PortraitManager();

        root = document.rootVisualElement;
        root.Clear();
        root.styleSheets.Add(styleSheet);
        root.AddToClassList("root");

        selectionChangedEventBinding = new EventBinding<SelectionChangedEvent>(BuildSelectedPortrait);
        playerLoadedEventBinding = new EventBinding<PlayerLoadedEvent>(BuildLayout);
    }

    private void OnEnable()
    {
        EventBus<SelectionChangedEvent>.Register(selectionChangedEventBinding);
        EventBus<PlayerLoadedEvent>.Register(playerLoadedEventBinding);
    }

    private void OnDisable()
    {
        EventBus<SelectionChangedEvent>.Deregister(selectionChangedEventBinding);
        EventBus<PlayerLoadedEvent>.Deregister(playerLoadedEventBinding);

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

        if (root != null)
        {
            //root.UnregisterCallback<MouseDownEvent>(HandleRootMouseDown, TrickleDown.TrickleDown);
        }
    }

    void BuildLayout(PlayerLoadedEvent evt)
    {
        overlayOwner = evt.playerStats.selectable;
        if (!layoutBuilt)
        {
            BuildUIPanel();
            CreateContextBox();

           // root.RegisterCallback<MouseDownEvent>(HandleRootMouseDown, TrickleDown.TrickleDown);

            layoutBuilt = true;
        }

        BuildHumanPlayerPortrait(evt.playerStats.selectable);
    }

    void BuildUIPanel()
    {
        humanPlayerOverlayHolder = new VisualElement();
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



        humanPlayerOverlayHolder.Add(humanPortraitHolder);
        humanPlayerOverlayHolder.Add(selectedPortraitHolder);
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

    /*
    void BuildSelectedPortrait(SelectionChangedEvent evt)
    {
       // Debug.Log($"clicked at world: {evt.mousePos}  local: {humanPlayerOverlayHolder.WorldToLocal(evt.mousePos)}");

        Vector2 mousePos = evt.mousePos;
        mousePos.y = Screen.height - mousePos.y;

        

        

        var elt = HandleMouseDown(mousePos);

        Portrait newSelectedPortrait = elt as Portrait;

     


        if (evt.selected == null) {
            if (elt == null) {
                // hide selected and context box
                ClearSelectedPortrait();
                return;
            }
        }

        if (evt.selected != null) {
            if (newSelectedPortrait != null) {
                // new selection, change selected portrait
                if (portraits.TryAddPortrait(evt.selected, out var portrait))
                {
                    ClearSelectedPortrait();
                    selectedPortrait = portrait;
                    selectedPortrait.showContextMenuOnPortrait += ShowContextBox;
                    selectedPortraitHolder.Add(selectedPortrait);
                }
            }
        }
    */
    //ShowContextBox(humanPlayerOverlayHolder.WorldToLocal(mousePos),evt.selected);
    /*
    if (evt.selected == null)
    {
        if (selectedPortrait != null && IsMouseOverElement(evt.mousePos, selectedPortrait))
        {
            Debug.Log("Null world selection, but click was on selected portrait UI. Keeping portrait.");
            return;
        }

        if (selectionContextBox != null &&
            selectionContextBox.style.display == DisplayStyle.Flex &&
            IsMouseOverElement(evt.mousePos, selectionContextBox))
        {
            Debug.Log("Null world selection, but click was on context box. Keeping portrait.");
            return;
        }

        ClearSelectedPortrait();
        HideContextBox();
        return;
    }

    if (selectedPortrait != null &&
        selectedPortrait.GetDisplayedPortrait() != null &&
        selectedPortrait.GetDisplayedPortrait().EntityRuntimeID == evt.selected.EntityRuntimeID)
    {
        Debug.Log("Same selected target, keeping portrait");
        return;
    }

    ClearSelectedPortrait();

    if (portraits.TryAddPortrait(evt.selected, out var portrait))
    {
        selectedPortrait = portrait;
        selectedPortrait.showContextMenuOnPortrait += ShowContextBox;
        selectedPortraitHolder.Add(selectedPortrait);
    }*/
    // }

    void BuildSelectedPortrait(SelectionChangedEvent evt)
    {
        Vector2 mousePos = evt.mousePos;
        mousePos.y = Screen.height - mousePos.y;

        VisualElement picked = humanPlayerOverlayHolder.panel.Pick(mousePos);
        Portrait clickedPortrait = GetTypeFromPickedElement<Portrait>(picked);
        PortraitContextMenu menu = GetTypeFromPickedElement<PortraitContextMenu>(picked);


        if (menu != null)
        {
            // clicked context menu → handle that first
            Debug.Log("hit menu");
        }
        else if (clickedPortrait != null && humanPortrait != null && clickedPortrait.id != humanPortrait.id)
        {
            HideContextBox();
            if (selectedPortrait != null && selectedPortrait.id == clickedPortrait.id)
                return;

            ClearSelectedPortrait();
            selectedPortrait = clickedPortrait;
            selectedPortrait.showContextMenuOnPortrait += ShowContextBox;
            selectedPortraitHolder.Add(selectedPortrait);
            return;
        }
        else
        {
            // clicked elsewhere
            // Clicked in the world and selected something
            if (evt.selected != null)
            {
                if (portraits.TryAddPortrait(evt.selected, out var portrait))
                {
                    ClearSelectedPortrait();
                    selectedPortrait = portrait;
                    selectedPortrait.showContextMenuOnPortrait += ShowContextBox;
                    selectedPortraitHolder.Add(selectedPortrait);
                }

                return;
            }

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
        if (selectedPortrait == null)
            return;

        selectedPortrait.showContextMenuOnPortrait -= ShowContextBox;
        selectedPortrait.Dispose();

        portraits.TryRemovePortrait(selectedPortrait.GetDisplayedPortrait(), out var _);

        selectedPortraitHolder.Clear();
        selectedPortrait = null;
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

    VisualElement HandleMouseDown(Vector2 mousePos)
    {

        var selectedElt = humanPlayerOverlayHolder.panel.Pick(mousePos);

        if (selectedElt != null && selectedElt != humanPlayerOverlayHolder && selectedElt != root)
        {
            Debug.Log($"selected {selectedElt}!");
            return selectedElt;
        }
        else {
            return null;
        }

        /*
        if (selectionContextBox == null ||
            selectionContextBox.style.display == DisplayStyle.None)
        {
            return;
        }

        var clicked = evt.target as VisualElement;
        if (clicked == null)
        {
            Debug.Log("clicked was null, hiding context box");
            HideContextBox();
            return;
        }

        if (IsInside(clicked, selectionContextBox))
        {
            Debug.Log("is inside");
            return;
        }

        var clickedPortrait = GetPortraitFromElement(clicked);
        if (clickedPortrait != null)
        {
            Debug.Log("portrait is clickjed");
            return;
        }

        HideContextBox();
        */
    }

    Portrait GetPortraitFromElement(VisualElement element)
    {
        VisualElement current = element;

        while (current != null)
        {
            if (current is Portrait portrait)
                return portrait;

            current = current.parent;
        }

        return null;
    }

    bool IsInside(VisualElement child, VisualElement possibleAncestor)
    {
        VisualElement current = child;

        while (current != null)
        {
            if (current == possibleAncestor)
                return true;

            current = current.parent;
        }

        return false;
    }
}