using PlasticPipe.PlasticProtocol.Messages;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadSlots : VisualElement
{
    VisualElement slotHolder;
    VisualElement slot1;
    VisualElement slot2;
    VisualElement slot3;
    VisualElement slot4;
    VisualElement slot5;

    List<GameSaveMetadata> metadata;

    Button backButton;

    public LoadSlots(List<GameSaveMetadata> metaData) { 
        this.metadata = metaData;
    }

    public void Initialize() {

        AddToClassList("loadSlots");

        BuildSlots();
        BuildBackButton();

        Add(slotHolder);
        Add(backButton);

        this.style.display = DisplayStyle.None;
    }

    void BuildSlots() {
        slotHolder = new VisualElement();
        slotHolder.AddToClassList("saveSlotHolder");

        foreach (var slotData in metadata) {
            var slot = new LoadSlot(slotData);
            slotHolder.Add(slot);
        }
    }

    void BuildBackButton() { 
        backButton = new Button();
        backButton.text = "Back";

        backButton.AddToClassList("backButton");
        backButton.clicked += () => HideSlots();

    }

    public void ShowSlots() {
        this.style.display = DisplayStyle.Flex;
    }

    public void HideSlots() {
        this.style.display = DisplayStyle.None;
    }

}
