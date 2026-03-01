using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestStages
{
    bool stagesComplete = false;
    QuestStageDetails currentStage;
    public event Action AllStagesCompleteEvent;

    public QuestStages(QuestStageDetails initialStage) { 
        currentStage = initialStage;
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
                AllStagesCompleteEvent?.Invoke();
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
