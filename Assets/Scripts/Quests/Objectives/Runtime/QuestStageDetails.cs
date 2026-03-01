using System;
using System.Collections.Generic;
using UnityEngine;


public class QuestStageDetails { 

    List<ObjectiveStageRequirement> preRequisites;
    List<ObjectiveStageRequirement> requirements;
    public bool complete = false;
    List<QuestStageDetails> possibleNextStages;

    public event Action<IReadOnlyList<QuestAction>> StageCompleteEvent;
    public event Action<IReadOnlyList<QuestAction>> StageStartedEvent;
    public event Action<IReadOnlyList<QuestAction>> RequirementCompleteEvent;
    public event Action<IReadOnlyList<QuestAction>> RequirementIncrementedEvent;

    IReadOnlyList<QuestAction> incrementActions;
    IReadOnlyList<QuestAction> finishedRequirementActions;
    IReadOnlyList<QuestAction> stageEndActions;
    IReadOnlyList<QuestAction> stageStartActions;

    List<QuestAction> actionSubsetHolder;
    

    public bool TryTransition(out QuestStageDetails nextStage) 
    {
        if (possibleNextStages.Count == 0)
        {
            nextStage = null;
            return false;
        }
        else {
            foreach (var stage in possibleNextStages)
            {
                if (stage.CheckPreReqs())
                {
                    StageCompleteEvent?.Invoke(stageEndActions);
                    nextStage = stage;
                    return true;
                }
            }
            // cant transition -- no event
            nextStage = null;
            return false;
        }
    }



    public void EnterStage() {
        Debug.Log("I have entered this stage");
        StageStartedEvent?.Invoke(stageStartActions);
    }

    public void RequestIncrementProgress(string id,int count) {
        if (!complete) {
            foreach (var requirement in requirements)
            {
                if (requirement.TryIncrementProgress(id, count))
                {
                    RequirementIncrementedEvent?.Invoke(incrementActions);
                    if (requirement.CheckCompletion())
                    {
                        RequirementCompleteEvent?.Invoke(finishedRequirementActions);
                    }
                }
            }
        }
        
    }

    public bool CheckPreReqs() {
        foreach (var preRequisite in preRequisites) {
            if (!preRequisite.CheckCompletion()) { 
                return false;
            }
        }
        return true;
    }

    public bool CheckStageCompletion() {
        if (complete == true) return true;
        foreach (var requirement in requirements) {
            if (!requirement.CheckCompletion()) {
                return false;
            }
        }
        complete = true;
        return true;
    }



}
