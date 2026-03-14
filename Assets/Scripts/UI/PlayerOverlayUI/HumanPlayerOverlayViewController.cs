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


    EventBinding<SelectionChangedEvent> selectionChangedEventBinding;

    List<Portrait> managedPortraits;

    private void Start()
    {
        root = document.rootVisualElement;
        root.Clear();
        managedPortraits = new List<Portrait>();
        //      .. allQuests = new List<QuestSettings>();
        root.styleSheets.Add(styleSheet);
        root.AddToClassList("root");

        selectionChangedEventBinding = new EventBinding<SelectionChangedEvent>(HandleSelectionChanged);

        EventBus<SelectionChangedEvent>.Register(selectionChangedEventBinding);

        BuildLayout();


    }

    void HandleSelectionChanged(SelectionChangedEvent evt) {
        //evt.selected.healthChangedEvent += portrait.UpdatePortraitHealth;
    }


    void BuildLayout() {
        BuildUIPanel();
        BuildPortrait();
    }

    private void AddPortrait(ISelectable newPortrait)
    {
       // var portrait = new Portrait(newPortrait);
    }

    void BuildPortrait() {
       // humanPortrait = new Portrait(100,100);
        humanPlayerOverlayHolder.Add(humanPortrait);
        managedPortraits.Add(humanPortrait);
    }

    void BuildUIPanel() { 
        humanPlayerOverlayHolder = new VisualElement();
        humanPlayerOverlayHolder.AddToClassList("overlay");
        root.Add(humanPlayerOverlayHolder);
    }
}
