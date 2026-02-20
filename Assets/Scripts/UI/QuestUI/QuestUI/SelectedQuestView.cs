using System;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectedQuestView : VisualElement
{
    VisualElement titleHolder;
    VisualElement descriptionHolder;

    VisualElement buttonsHolder;
    Button acceptButton;
    Button closeButton;
    Label descriptionText;
    Label titleText;
    VisualElement backPanel;
    //QuestSettings selectedQuest;



    public event Action DeclinedQuest;
    public event Action AcceptedQuest;


    public SelectedQuestView() {
        backPanel = new VisualElement();
        backPanel.AddToClassList("backPanel");

        titleHolder = new VisualElement();
        titleHolder.AddToClassList("titleHolder");
        titleText = new Label();

        titleHolder.Add(titleText);

        descriptionHolder = new VisualElement();
        descriptionHolder.AddToClassList("descriptionHolder");
        descriptionText = new Label();

        descriptionHolder.Add(descriptionText);

        buttonsHolder = new VisualElement();
        buttonsHolder.AddToClassList("buttonsHolder");
        
        acceptButton = new Button(() => { OnAcceptQuest(); })
        {
            text = "Accept"
        };
        closeButton = new Button(() => { HandleCloseView(); })
        {
            text = "Decline"
        };

        buttonsHolder.AddToClassList("buttonsHolder");
        buttonsHolder.Add(acceptButton);
        buttonsHolder.Add(closeButton);

        backPanel.Add(titleHolder);
        backPanel.Add(descriptionHolder);
        backPanel.Add(buttonsHolder);
        this.Add(backPanel);
        this.style.position = Position.Absolute;

        this.style.display = DisplayStyle.None;
    }

    public void HandleCloseView() { 
        DeclinedQuest?.Invoke();
    }
    
    public void ShowSelectedQuest(QuestUIItem quest) {
        titleText.text = quest.title;
        descriptionText.text = quest.description;

        this.style.display = DisplayStyle.Flex;
    }

    public void HideSelectedQuest() {
        titleText.text = "";
        descriptionText.text = "";

        this.style.display = DisplayStyle.None;
    }

    public void OnAcceptQuest() { 
        AcceptedQuest?.Invoke();
    }
}
