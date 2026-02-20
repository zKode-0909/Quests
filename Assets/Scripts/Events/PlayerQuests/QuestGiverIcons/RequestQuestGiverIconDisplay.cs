using UnityEngine;

public struct RequestQuestGiverIconDisplay : IEvent
{
    public int QuestGiverEntityRuntimeID;
    public int QuesterEntityRuntimeID;
    public int QuesterLevel;

    public RequestQuestGiverIconDisplay(int giverID,int questerID,int questerLevel) { 
        this.QuestGiverEntityRuntimeID = giverID;
        this.QuesterEntityRuntimeID = questerID;
        this.QuesterLevel = questerLevel;
    }
}
