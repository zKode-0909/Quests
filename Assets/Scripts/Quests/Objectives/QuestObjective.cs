using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjective
{
   // public QuestObjectiveType currentObjectiveType;
    public List<string> objectiveIncrementers;
    public string currentObjectiveIncrementer;
    public int maxProgress;
    public int currentProgress;
    public int questStage;

    Dictionary<int, QuestObjectiveDetails> objectiveStageTargets;

    

    public QuestObjective(List<QuestObjectiveDetails> objectiveOrderedList) {
        BuildObjectives(objectiveOrderedList);
        currentObjectiveIncrementer = objectiveOrderedList[0].objectiveItemStableID;
        maxProgress = objectiveOrderedList[0].maxObjectiveProgress;
        currentProgress = 0;
        questStage = 1;
       
    }

    public bool TryIncrementProgress(int objectiveThingID) {
        if (currentProgress == maxProgress || objectiveStageTargets[questStage].objectiveItemStableID != currentObjectiveIncrementer)
        {
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
        if (objectiveStageTargets.Count <= questStage)
        {
            questStage++;
            currentProgress = 0;
            maxProgress = objectiveStageTargets[questStage].maxObjectiveProgress;
            currentObjectiveIncrementer = objectiveStageTargets[questStage].objectiveItemStableID;
        }
        else {
            Debug.Log("You have completed all objectives! turn in the quest now");
        }
        

    }


    public void BuildObjectives(List<QuestObjectiveDetails> questDetails) {
        int idx = 0;
        objectiveStageTargets = new Dictionary<int, QuestObjectiveDetails>();
        foreach (var questDetail in questDetails) {
            objectiveStageTargets.Add(idx + 1, questDetails[idx]);
        }
    }

    



    
}
