using UnityEngine;


public class ObjectiveStageRequirement
{
    int maxProgessCount;
    int currentProgress;
    string questObjectiveStableID;

    public ObjectiveStageRequirement(int maxProgessCount, int currProgress, string questObjectiveStableID)
    {
        this.maxProgessCount = maxProgessCount;
        this.currentProgress = 0;
        this.questObjectiveStableID = questObjectiveStableID;
    }

    public string GetQuestObjectiveStableID() {
        return questObjectiveStableID;
    }

    public int GetMaxProgressCount() { 
        return maxProgessCount;
    }

    public int GetCurrentProgress() {
        return currentProgress;
    }

    public bool CheckCompletion() {
        return currentProgress >= maxProgessCount; 
    }

    public bool TryIncrementProgress(string id,int count) {
        if (currentProgress < maxProgessCount && questObjectiveStableID == id) {
            currentProgress += count;
            return true;
        } 

        return false;

    }

}
