using UnityEngine;

public struct RequestAcceptQuest : IEvent
{
    public int AccepterEntityRuntimeID;
    public int GiverEntityRuntimeID;
    public string QuestID;

    public RequestAcceptQuest(int accepterID, int giverID, string questID) { 
        this.AccepterEntityRuntimeID = accepterID;
        this.GiverEntityRuntimeID = giverID;
        this.QuestID = questID;
    }

}
