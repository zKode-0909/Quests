using UnityEngine;

public struct IncrementPlayerQuestLogUI : IEvent
{
    public string objectiveID;
    public int progress;

    public IncrementPlayerQuestLogUI(string objectiveID, int progress) { 
        this.objectiveID = objectiveID;
        this.progress = progress;
    }

}
