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
    QuestDetailedView questDetailedView;

    List<QuestUIItem> currentQuests;

    bool questBeingShown;

    EventBinding<IncrementPlayerQuestLogUI> incrementProgress;


    public QuestLogView(VisualElement root, StyleSheet styleSheet) {
        this.root = root;
        this.styleSheet = styleSheet;
    }

    public void Initialize() {
        root.Clear();

        root.styleSheets.Add(styleSheet);

        incrementProgress = new EventBinding<IncrementPlayerQuestLogUI>(HandleIncrement);

        EventBus<IncrementPlayerQuestLogUI>.Register(incrementProgress);

        BuildQuestLogView();
        root.AddToClassList("questLogDisplay");
    }

    public void Dispose() {
        root.Clear();
    }

    void HandleIncrement(IncrementPlayerQuestLogUI evt) {
        if (currentQuests != null) {
            foreach (var quest in currentQuests)
            {
                foreach (var requirement in quest.objectives)
                {

                    if (requirement.objectiveID == evt.objectiveID)
                    {
                        requirement.objectiveProgress += 1;
                        questDetailedView.UpdateProgress(requirement.objectiveID, requirement);
                    }


                }



            }
        }
        

      
        
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

        questDetailedView = new QuestDetailedView();    
        questDetailedView.Initialize();

        root.Add(questDetailedView);

        root.style.display = DisplayStyle.None;

    }
    
    public void OpenQuestLog(DisplayQuestLogEvent evt) {
        root.style.display = DisplayStyle.Flex;
        
        currentQuests = evt.Quests;
       
        foreach (var quest in currentQuests) {
            Debug.Log($"Quest {quest.questID} has: {quest.objectives.Count} objectives in quest log opening");
            var questVE = new QuestVE(quest);
            questVE.QuestClicked += ShowDetailedQuest;
            bodyHolder.Add(questVE);
        }
    }

    
    public void CloseQuestLog() { 
        bodyHolder.Clear();
        root.style.display = DisplayStyle.None;
    }

    public void ShowDetailedQuest(QuestUIItem quest) { 
        questBeingShown = true;
        questDetailedView.ShowQuestView(quest);
    }
}
