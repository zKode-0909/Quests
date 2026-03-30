using UnityEngine;

public class QuestRequirementProgress
{
    public string objectiveID;
    public int objectiveProgress;
    public int maxObjectiveProgress;

    public QuestRequirementProgress(string objective,int currentProgress,int maxProgress) { 
        this.objectiveID = objective;
        this.objectiveProgress = currentProgress;
        this.maxObjectiveProgress = maxProgress;
    }
}
