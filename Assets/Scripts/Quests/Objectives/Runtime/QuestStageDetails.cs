using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;


public class QuestStageDetails { 

    IReadOnlyList<ObjectiveStageRequirement> preRequisites;
    IReadOnlyList<ObjectiveStageRequirement> requirements;
    IReadOnlyList<IReadOnlyList<ObjectiveStageRequirement>> stageCompletionConditions;
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

    Dictionary<string,ObjectiveStageRequirement> reqById;

    List<QuestAction> actionSubsetHolder;

    public QuestStageDetails(IReadOnlyList<ObjectiveStageRequirement> preReqs,IReadOnlyList<ObjectiveStageRequirement> reqs,
        IReadOnlyList<IReadOnlyList<ObjectiveStageRequirement>> conditions,
        IReadOnlyList<QuestAction> incrementActions,IReadOnlyList<QuestAction> finishedRequirementActions,
        IReadOnlyList<QuestAction> stageEndActions,IReadOnlyList<QuestAction> stageStartActions) 
    {
        this.preRequisites = preReqs;
        this.requirements = reqs;
        this.stageCompletionConditions = conditions;
        this.incrementActions = incrementActions;
        this.finishedRequirementActions = finishedRequirementActions;
        this.stageEndActions = stageEndActions;
        this.stageStartActions = stageStartActions;

        reqById = new Dictionary<string,ObjectiveStageRequirement>();

        foreach (var req in requirements)
            reqById[req.GetQuestObjectiveStableID()] = req;

    }

    public void SetNextStages(List<QuestStageDetails> nextStages) { 
        this.possibleNextStages = nextStages;
    }

    public bool TryTransition(Dictionary<string,int> progressDict,out QuestStageDetails nextStage) 
    {
        if (possibleNextStages.Count == 0)
        {
            nextStage = null;
            return false;
        }
        else {
            foreach (var stage in possibleNextStages)
            {
                if (stage.CheckPreReqs(progressDict))
                {
                    StageCompleteEvent?.Invoke(stageEndActions);
                    Debug.Log("transitioning stage");
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

    public void RequestIncrementProgress(string id,int count,Dictionary<string,int> progressDict) {

        

        
        if (!complete) {
            foreach (var requirement in requirements)
            {
                if (progressDict.TryGetValue(id,out var progress) && requirement.GetQuestObjectiveStableID() == id)
                {
                    if (progress < requirement.GetMaxProgressCount()) {
                        progressDict[id] += count;
                        RequirementIncrementedEvent?.Invoke(incrementActions);
                        Debug.Log($"incremented requirement: {id}");
                        if (requirement.CheckCompletion(progressDict))
                        {
                            Debug.Log($"completed requirement: {id}");
                            RequirementCompleteEvent?.Invoke(finishedRequirementActions);
                        }
                    }
                }
            }
        }
        
    }

    public bool CheckPreReqs(Dictionary<string,int> progressDict) {
        foreach (var preRequisite in preRequisites) {
            if (!preRequisite.CheckCompletion(progressDict)) { 
                return false;
            }
        }
        return true;
    }

    public bool CheckStageCompletion(Dictionary<string, int> progressDict) {
        if (complete == true) return true;

        

        foreach (var conditionGroup in stageCompletionConditions)
        {
            bool groupSatisfied = true;

            foreach (var condReq in conditionGroup)
            {
                var id = condReq.GetQuestObjectiveStableID();

                // must exist and be complete
                if (!reqById.TryGetValue(id, out var runtimeReq) || !runtimeReq.CheckCompletion(progressDict))
                {
                    groupSatisfied = false;
                    break; // fail this group; try next group
                }
            }

            if (groupSatisfied)
            {
                complete = true;
                return true;
            }
        }
        return false;



        /*

        foreach (var requirement in requirements) {
            if (!requirement.CheckCompletion(progressDict)) {
                return false;
            }
        }
        complete = true;
        return true;*/
    }



}
