using UnityEngine;

public readonly struct QuestObjectiveDetails
{
    public readonly string objectiveItemStableID;
    public readonly int maxObjectiveProgress;

    public QuestObjectiveDetails(string itemID,int maxProg) { 
        this.objectiveItemStableID = itemID;
        this.maxObjectiveProgress = maxProg;
    }

}
