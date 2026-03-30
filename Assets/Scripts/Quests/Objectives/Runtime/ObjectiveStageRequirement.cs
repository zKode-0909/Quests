using System.Collections.Generic;
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

    public void SetCurrentProgress(int progress) { 
        currentProgress = progress;
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

    public bool CheckCompletion(Dictionary<string,int> progressDict) {
        if (progressDict.TryGetValue(questObjectiveStableID, out var progress))
        {
            return progress >= maxProgessCount;
        }
        else {
            return false; 
        }
        
    }

    public bool TryIncrementProgress(string id,int count) {
        if (currentProgress < maxProgessCount && questObjectiveStableID == id) {
            currentProgress += count;
            return true;
        } 

        return false;

    }

}
