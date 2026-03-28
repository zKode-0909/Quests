using UnityEngine;

public struct RequestQuestGiverIconDisplay : IEvent
{
    public string QuestGiverEntityStableID;
    public string QuesterEntityStableID;
    public int QuesterLevel;

    public RequestQuestGiverIconDisplay(string giverID,string questerID,int questerLevel) { 
        this.QuestGiverEntityStableID = giverID;
        this.QuesterEntityStableID = questerID;
        this.QuesterLevel = questerLevel;
    }
}
