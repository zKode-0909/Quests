using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestObjective
{
   // public QuestObjectiveType currentObjectiveType;
    //public string currentObjectiveIncrementer;
   // public int maxProgress;
    //public int currentProgress;

    //Dictionary<int, QuestObjectiveDetails> objectiveStageTargets;



    public List<QuestStageDetails> Stages { get; }

    public QuestObjective(List<QuestStageDetails> stages) => Stages = stages;

    public bool TryGetMatchingRequirementId(int stageIndex, string objectiveThingId, out string stableId)
    {
        foreach (var obj in Stages[stageIndex].requirements)
        {
            if (CheckObjectiveValidity(objectiveThingId, obj.requirement))
            {
                stableId = obj.requirement.GetQuestObjectiveStableID();
                return true;
            }
        }
        stableId = null;
        return false;
    }

    public bool IsStageComplete(int stageIndex, IReadOnlyDictionary<string, int> progress)
    {
        Debug.Log($"stage index is: {stageIndex}");
        foreach (var obj in Stages[stageIndex].requirements)
        {
            var req = obj.requirement;
            if (progress.TryGetValue(req.GetQuestObjectiveStableID(), out var p) == false) return false;
            if (p < req.GetMaxProgressCount()) return false;
        }
        return true;
    }

    bool CheckObjectiveValidity(string objectiveID, ObjectiveStageRequirement requirement)
    {
        if (requirement.GetQuestObjectiveStableID() == objectiveID) return true;
        return false;

    }


    /*
    public void TryIncrementProgress(string objectiveThingID) {
        var objectiveDetails = objectiveStageTargets[questStage - 1].requirements;

        bool stageComplete = true;

        foreach (var objective in objectiveDetails)
        {
            var requirement = objective.requirement;

            if (CheckObjectiveValidity(objectiveThingID, requirement))
                requirement.IncrementProgress(1);

            if (requirement.GetProgress() < requirement.GetMaxProgressCount())
                stageComplete = false;
        }

        if (stageComplete)
            IncrementStage();


    }

   

    void IncrementStage() {
        if (questStage < objectiveStageTargets.Count)
        {
            
            questStage++;
        }
        else
        {
            Debug.Log("You have completed all objectives! turn in the quest now");
        }


    }
    */
    /*
    public void BuildObjectives(List<QuestObjectiveDetails> questDetails) {
        int idx = 0;
        objectiveStageTargets = new Dictionary<int, QuestObjectiveDetails>();
        foreach (var questDetail in questDetails) {
            objectiveStageTargets.Add(idx + 1, questDetails[idx]);
            idx++;
        }
    }*/






}
