using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestStages
{
    bool stagesComplete = false;
    QuestStageDetails currentStage;
    IReadOnlyList<QuestAction> allObjectivesFinishedActions;

    public event Action<IReadOnlyList<QuestAction>> QuestObjectiveEvent;
    
    

    public QuestStages(QuestStageDetails initialStage) { 
        currentStage = initialStage;
        currentStage.StageStartedEvent += SendActions;
        currentStage.RequirementCompleteEvent += SendActions;
        currentStage.StageCompleteEvent += SendActions;
        currentStage.RequirementIncrementedEvent += SendActions;
    }


    void SendActions(IReadOnlyList<QuestAction> actions) {
        QuestObjectiveEvent?.Invoke(actions);
    }

    bool TryTransition() {
        if (currentStage.CheckStageCompletion()) {
            if (currentStage.TryTransition(out var nextStage))
            {
                currentStage = nextStage;
                nextStage.EnterStage();
                return true;
            }
            else {
                
                stagesComplete = true;
                SendActions(allObjectivesFinishedActions);
                return false;
            }
        }
        return false;
    }

    public void RequestIncrementObjective(string id, int count) {
        if (stagesComplete == false) {
            currentStage.RequestIncrementProgress(id, count);
            TryTransition();
        }
        
    } 

    public bool GetQuestCompletionStatus() => stagesComplete;

}
