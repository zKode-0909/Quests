using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestStages
{
    bool stagesComplete = false;
    QuestStageDetails currentStage;
    IReadOnlyList<QuestAction> allObjectivesFinishedActions;

    public event Action ObjectivesFinishedEvent;

    public event Action<IReadOnlyList<QuestAction>> QuestObjectiveEvent;

    Dictionary<string, int> progressByTargetId;



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
        if (currentStage.CheckStageCompletion(progressByTargetId)) {
            if (currentStage.TryTransition(progressByTargetId,out var nextStage))
            {
                currentStage = nextStage;
                nextStage.EnterStage();
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
