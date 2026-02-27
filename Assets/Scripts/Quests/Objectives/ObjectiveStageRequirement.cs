using UnityEngine;

[System.Serializable]
public class ObjectiveStageRequirement
{
    [SerializeField ] int maxProgessCount;
    [SerializeField] string questObjectiveStableID;

    public ObjectiveStageRequirement(int maxProgessCount, int currProgress, string questObjectiveStableID)
    {
        this.maxProgessCount = maxProgessCount;
        this.questObjectiveStableID = questObjectiveStableID;
    }

    public string GetQuestObjectiveStableID() {
        return questObjectiveStableID;
    }

    public int GetMaxProgressCount() { 
        return maxProgessCount;
    }

}
