using Mono.Cecil;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestGiverView : VisualElement
{
    VisualElement backPanel;
    VisualElement questGiverTextHolder;
    Label questGiverBodyText;
    VisualElement questButtonsHolder;
    VisualElement displayButtonsHolder;
    QuestHandoutView parent;
    Button closeButton;
    public event Action<QuestUIItem> QuestSelected;
    public event Action CloseView;

    public QuestGiverView(QuestHandoutView parent)
    {
        backPanel = new VisualElement();
        backPanel.style.flexDirection = FlexDirection.Column;
        backPanel.AddToClassList("backPanel");


        questGiverTextHolder = new VisualElement();
        questGiverBodyText = new Label();
        questGiverTextHolder.Add(questGiverBodyText);
        questGiverTextHolder.style.display = DisplayStyle.Flex;
        questGiverTextHolder.AddToClassList("questGiverText");


        questButtonsHolder = new VisualElement();
        questButtonsHolder.style.display = DisplayStyle.Flex;
        questButtonsHolder.style.flexDirection = FlexDirection.Column;
        questButtonsHolder.AddToClassList("questList");

        displayButtonsHolder = new VisualElement();
        displayButtonsHolder.style.display = DisplayStyle.Flex;
        displayButtonsHolder.style.flexDirection = FlexDirection.Row;
        closeButton = new Button(() => { OnClose(); });
        displayButtonsHolder.Add(closeButton);
        displayButtonsHolder.AddToClassList("questGiverDisplayButtons");

        backPanel.Add(questGiverTextHolder);
        backPanel.Add(questButtonsHolder);
        backPanel.Add(displayButtonsHolder);

        this.Add(backPanel);
        this.style.position = Position.Absolute;
        this.parent = parent;

        this.style.display = DisplayStyle.None;
        this.AddToClassList("questGiverView");
    }
    
    public void ShowQuestGiverView(IReadOnlyList<QuestUIItem> quests, string questerName,string questGiverName) {
        this.style.display = DisplayStyle.Flex;
        backPanel.style.display = DisplayStyle.Flex;
        questGiverBodyText.text = "I forgot text: TEST TEXT";//questGiver.questGiverText;
      
        foreach (var quest in quests) {
           //QuestUtils.DetermineQuestStatus(quest, quester);

            var questHolder = new VisualElement();
            
            var questText = new Label();

            questHolder.Add(questText);

            Debug.Log($"status is: {quest.status}");
            switch (quest.status) { 
                case QuestStatus.NotStarted: AddQuestButton(quest.status, quest); break;
                case QuestStatus.InProgress: AddQuestButton(quest.status, quest); break;
                default: ; break;
            }

            
             
        }




    }

    void AddQuestButton(QuestStatus questStatus,QuestUIItem quest) {
        var questButton = new Button(() => { OnQuestSelected(quest); })
        {
            text = $"{quest.title}: {questStatus}"
        };
        questButton.AddToClassList("quest");
        questButtonsHolder.Add(questButton);
    }
    
    public void OnClose() {
        CloseView.Invoke();
    }
    
    
    void OnQuestSelected(QuestUIItem quest) {
        QuestSelected.Invoke(quest);
    }

    public void CloseQuestGiverView() {
        this.style.display = DisplayStyle.None;
        questGiverBodyText.text = "";
        questButtonsHolder.Clear();
    }

    


}
