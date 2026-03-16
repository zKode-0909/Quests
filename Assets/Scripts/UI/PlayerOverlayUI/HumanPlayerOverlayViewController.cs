using Codice.Client.BaseCommands.CheckIn;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HumanPlayerOverlayViewController : MonoBehaviour
{
    [SerializeField] UIDocument document;
    [SerializeField] StyleSheet styleSheet;

    VisualElement root;

    Portrait humanPortrait;
    Portrait selectedPortrait;
    VisualElement humanPlayerOverlayHolder;
    PortraitManager portraits;


    EventBinding<SelectionChangedEvent> selectionChangedEventBinding;
    EventBinding<PlayerLoadedEvent> playerLoadedEventBinding;

  

    private void Start()
    {
        portraits = new PortraitManager();

        root = document.rootVisualElement;
        root.Clear();
        //      .. allQuests = new List<QuestSettings>();
        root.styleSheets.Add(styleSheet);
        root.AddToClassList("root");

        selectionChangedEventBinding = new EventBinding<SelectionChangedEvent>(BuildSelectedPortrait);
        playerLoadedEventBinding = new EventBinding<PlayerLoadedEvent>(BuildLayout);

        EventBus<SelectionChangedEvent>.Register(selectionChangedEventBinding);
        EventBus<PlayerLoadedEvent>.Register(playerLoadedEventBinding);

        //BuildLayout();


    }



    void BuildLayout(PlayerLoadedEvent evt) {
        BuildUIPanel();
        BuildHumanPlayerPortrait(evt.playerStats.selectable);
    }

    private void BuildSelectedPortrait(SelectionChangedEvent evt)
    {
        if (selectedPortrait != null) {
            portraits.TryRemovePortrait(selectedPortrait.GetDisplayedPortrait(), out var old);
            
        }
        


        if (portraits.TryAddPortrait(evt.selected, out var portrait)) {
            selectedPortrait.Clear();
            selectedPortrait = portrait;
            
     
            
        }
    }

    void BuildHumanPlayerPortrait(ISelectable selectable) {

        if (humanPortrait != null) {
            portraits.TryRemovePortrait(humanPortrait.GetDisplayedPortrait(), out var old);
            
        }
        

        if (portraits.TryAddPortrait((selectable), out var portrait)) {
            humanPortrait.Clear();
            humanPortrait = portrait;
            
        }
        
        
    }

    void BuildUIPanel() { 
        humanPlayerOverlayHolder = new VisualElement();
        humanPlayerOverlayHolder.AddToClassList("overlay");
        humanPlayerOverlayHolder.Add(humanPortrait);
        humanPlayerOverlayHolder.Add(selectedPortrait);
        root.Add(humanPlayerOverlayHolder);
    }
}
