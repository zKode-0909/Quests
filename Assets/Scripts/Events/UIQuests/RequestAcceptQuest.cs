using UnityEngine;

public struct RequestAcceptQuest : IEvent
{
    public string AccepterEntityStableID;
    public string GiverEntityStableID;
    public string QuestID;

    public RequestAcceptQuest(string accepterID, string giverID, string questID) { 
        this.AccepterEntityStableID = accepterID;
        this.GiverEntityStableID = giverID;
        this.QuestID = questID;
    }

}
