using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestLogView : VisualElement
{

    StyleSheet styleSheet;
    VisualElement root;
    VisualElement titleHolder;
    Label titleLabel;
    VisualElement bodyHolder;
    VisualElement questLogHolder;


    public QuestLogView(VisualElement root, StyleSheet styleSheet) {
        this.root = root;
        this.styleSheet = styleSheet;
    }

    public void Initialize() {
        root.Clear();

        root.styleSheets.Add(styleSheet);

        BuildQuestLogView();
        root.AddToClassList("questLogDisplay");
    }

    public void Dispose() {
        root.Clear();
    }

    



    void BuildQuestLogView() {
        questLogHolder = new VisualElement();
        questLogHolder.AddToClassList("questLogHolder");

        titleHolder = new VisualElement();
        titleLabel = new Label();
        titleLabel.text = "QUEST LOG";
        titleHolder.AddToClassList("titleHolder");
        titleHolder.Add(titleLabel);

        bodyHolder = new VisualElement();
        bodyHolder.AddToClassList("questLogBody");
        questLogHolder.Add(titleHolder);
        questLogHolder.Add(bodyHolder);
        root.Add(questLogHolder);

        root.style.display = DisplayStyle.None;

    }
    
    public void OpenQuestLog(DisplayQuestLogEvent evt) {
        root.style.display = DisplayStyle.Flex;
        var quests = evt.Quests;

        foreach (var quest in quests) {
            var questVE = new QuestVE(quest.title,quest.questID,quest.status);
            bodyHolder.Add(questVE);
        }
    }

    
    public void CloseQuestLog() { 
        bodyHolder.Clear();
        root.style.display = DisplayStyle.None;
    }
}
