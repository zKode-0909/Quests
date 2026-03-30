using Codice.CM.SEIDInfo;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;

public class QuestDetailedView : VisualElement
{
    VisualElement titleHolder;
    VisualElement descHolder;
    VisualElement progressHolder;
    VisualElement rewardsHolder;

    Button backButton;

    Label titleText;
    Label descText;
   
    Label rewardsText;

    QuestUIItem currentQuest;

    List<QuestRequirementProgress> currentProgress;
    List<ProgressText> progress;

    string questTitle;
    string questDescription;

    public string currentQuestID;

    public QuestDetailedView() {
        AddToClassList("detailedQuestView");
    }

    public void Initialize() {
        BuildQuestView();
        progress = new List<ProgressText>();    
        this.style.display = DisplayStyle.None;
    }

    public void ShowQuestView(QuestUIItem quest) { 

        progressHolder.Clear();

        titleText.text = quest.title;

        descText.text = quest.description;

        currentQuestID = quest.questID;

        Debug.Log($"Quest has: {quest.objectives.Count} objectives");
        currentProgress = new List<QuestRequirementProgress>();
        foreach (var requirement in quest.objectives) {

            Debug.Log($"Quest objective: {requirement.objectiveID}");
            var questObjectiveHolder = new ProgressText(requirement.objectiveID);
            var questObjectiveText = $"{requirement.objectiveID}: {requirement.objectiveProgress}/{requirement.maxObjectiveProgress}";
            questObjectiveHolder.UpdateText(questObjectiveText);
            progress.Add(questObjectiveHolder);
          
            currentProgress.Add(requirement);
            progressHolder.Add(questObjectiveHolder);
        
        
        }

   

        rewardsText.text = "Rewards Text Placeholder";

        this.style.display = DisplayStyle.Flex;

        currentQuest = quest;
        
      
    }

    public void UpdateProgress(string idToUpdate,QuestRequirementProgress requirementProgress) {
        foreach (var requirement in progress) {
            if (requirement.id == idToUpdate) {
                requirement.UpdateText($"{requirementProgress.objectiveID}: {requirementProgress.objectiveProgress}/{requirementProgress.maxObjectiveProgress}");

            }
        } 
       
    }



    void BuildQuestView() {
        titleHolder = new VisualElement();
        titleHolder.AddToClassList("detailedQuestTitle");
        titleText = new Label();
        titleHolder.Add(titleText);



        descHolder = new VisualElement();
        descHolder.AddToClassList("detailedQuestDescription");
        descText = new Label();
        descHolder.Add(descText);

        progressHolder = new VisualElement();
        progressHolder.AddToClassList("detailedProgressDisplay");

        rewardsHolder = new VisualElement();
        rewardsHolder.AddToClassList("detailedQuestRewards");
        rewardsText = new Label("Rewards Text Placeholder");
        rewardsHolder.Add(rewardsText);

        backButton = new Button();

        backButton.clicked += () => OnBackClicked();

        Add(titleHolder);
        Add(descHolder);
        Add(progressHolder);
        Add(rewardsHolder);
        Add(backButton);
    }

    

    void OnBackClicked() { 
        titleText.text = string.Empty;
        descText.text = string.Empty;
        progress.Clear();
        progressHolder.Clear();
        rewardsText.text = string.Empty;

        this.style.display = DisplayStyle.None;
    }

    
}
