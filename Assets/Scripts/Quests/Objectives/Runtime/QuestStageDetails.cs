using System.Collections.Generic;
using UnityEngine;


public class QuestStageDetails { 

    readonly List<ObjectiveStageRequirement> preRequisites;
    readonly List<ObjectiveStageRequirement> requirements;
    public bool complete = false;
    readonly List<QuestStageDetails> possibleNextStages;

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
                    // transition stage -- stage end event
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
        // enter stage -- stage start event
    }

    public void RequestIncrementProgress(string id,int count) {
        if (!complete) {
            foreach (var requirement in requirements)
            {
                if (requirement.TryIncrementProgress(id, count))
                {
                    // succesfully incremented requirement -- increment requirement event
                    if (requirement.CheckCompletion())
                    {
                        // finished requirement -- finished requirement event
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
