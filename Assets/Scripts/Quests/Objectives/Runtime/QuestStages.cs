using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestStages
{
    bool stagesComplete = false;
    QuestStageDetails currentStage;
    List<QuestStageDetails> allStages;
    IReadOnlyList<QuestAction> allObjectivesFinishedActions;

    public event Action ObjectivesFinishedEvent;

    public event Action<IReadOnlyList<QuestAction>> QuestObjectiveEvent;

    Dictionary<string, int> progressByTargetId;

    Inventory currentPlayerInventory;

    Quest currentQuest;

    public QuestStages(QuestStageDetails initialStage,IReadOnlyList<QuestAction> objectivesFinishedActions,List<QuestStageDetails> allStages) { 
        currentStage = initialStage;
        this.allStages = allStages;
  
        allObjectivesFinishedActions = objectivesFinishedActions;
        currentStage.StageStartedEvent += SendActions;
        currentStage.RequirementCompleteEvent += SendActions;
        currentStage.StageCompleteEvent += SendActions;
        currentStage.RequirementIncrementedEvent += SendActions;
    }

    public void SetInventory(Inventory inventory) {
        this.currentPlayerInventory = inventory;
    }

    public void SetQuest(Quest quest) { 
        this.currentQuest = quest;
    }

    public void SetStage(string id,List<QuestStageRequirementContext> stageCtx) {
        foreach (var stage in allStages) {
            if (stage.GetStageID() == id) { 
                currentStage = stage;
                break;
            }
        }

        foreach (var ctx in stageCtx) {
            foreach (var requirement in currentStage.GetStageRequirements())
            {
                if (requirement.Value.GetQuestObjectiveStableID() == ctx.ObjectiveID) {
                    requirement.Value.SetCurrentProgress(ctx.Progress);
                }
            }
        }
        
    }

    public QuestStageDetails GetCurrentStage() { 
        return currentStage;
    }

    public void SetStageProgress(List<QuestStageRequirementContext> stageCtx) { 
        
    }


    void SendActions(IReadOnlyList<QuestAction> actions) {
        QuestObjectiveEvent?.Invoke(actions);
    }

    bool TryTransition() {
        if (currentStage.CheckStageCompletion(progressByTargetId)) {
            if (currentStage.TryTransition(progressByTargetId,out var nextStage))
            {
                currentStage = nextStage;
                nextStage.EnterStage();
                foreach (var item in currentPlayerInventory.GetItems()) {
                    foreach (var requirement in nextStage.GetStageRequirements()) {
                        Debug.Log($"checking item: {item.StableID} vs objective {requirement.Value.GetQuestObjectiveStableID()}");
                        if (requirement.Value.GetQuestObjectiveStableID() == item.StableID) {
                            currentQuest.OnObjectiveEvent(item.StableID);
                        }
                    }
                }
                return true;
            }
            else {
                
                stagesComplete = true;
                Debug.Log($"QUEST OBJECTIVE COMPLETE!");
                ObjectivesFinishedEvent?.Invoke();
                SendActions(allObjectivesFinishedActions);
                return false;
            }
        }
        return false;
    }

    public void RequestIncrementObjective(string id, int count) {
        if (stagesComplete == false) {
            currentStage.RequestIncrementProgress(id, count,progressByTargetId);
            TryTransition();
        }
        
    }

    public void SetTargetIDDict(Dictionary<string, int> dict) { 
        progressByTargetId = dict;
    }

    public bool GetQuestCompletionStatus() => stagesComplete;

}
