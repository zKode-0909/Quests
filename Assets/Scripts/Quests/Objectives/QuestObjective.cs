using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjective
{
   // public QuestObjectiveType currentObjectiveType;
    public List<string> objectiveIncrementers;
    //public string currentObjectiveIncrementer;
    public int maxProgress;
    public int currentProgress;
    public int questStage;

    //Dictionary<int, QuestObjectiveDetails> objectiveStageTargets;

    readonly List<QuestObjectiveDetails> objectiveStageTargets;

    

    public QuestObjective(List<QuestObjectiveDetails> objectiveOrderedList) {
        //BuildObjectives(objectiveOrderedList);
        objectiveStageTargets = objectiveOrderedList;
        //currentObjectiveIncrementer = objectiveOrderedList[0].objectiveItemStableID;
        if (objectiveOrderedList.Count > 0) { maxProgress = objectiveOrderedList[0].maxObjectiveProgress; }
        
        
        currentProgress = 0;
        questStage = 1;
       
    }

    public bool TryIncrementProgress(string objectiveThingID) {
        if (currentProgress == maxProgress || objectiveStageTargets[questStage-1].objectiveItemStableID != objectiveThingID)
        {
            Debug.Log($"not able to increment objective currentProg: {currentProgress}," +
                $" maxProg: {maxProgress} ," +
                $" currentStageTarget: {objectiveStageTargets[questStage-1].objectiveItemStableID}" +
                $" objective passed in: {objectiveThingID}");
            return false;
        }
        else {
            currentProgress++;
            
            if (currentProgress >= maxProgress) {
                IncrementStage();
            }
            return true;
        }
    
    }

    void IncrementStage() {
        if (questStage < objectiveStageTargets.Count)
        {
            questStage++;
            currentProgress = 0;
            maxProgress = objectiveStageTargets[questStage-1].maxObjectiveProgress;
        }
        else
        {
            Debug.Log("You have completed all objectives! turn in the quest now");
        }


    }

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
