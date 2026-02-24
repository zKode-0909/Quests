using UnityEngine;

public readonly struct QuestUIItem 
{
    public readonly string title;
    public readonly string description;
    public readonly int questGiverID;
    public readonly string questID;
    public readonly string status;

    public QuestUIItem(string title,string description,int giverID,string questID,string status) { 
        this.title = title;
        this.description = description;
        this.questGiverID = giverID;
        this.questID = questID;
        this.status = status;

    }

}
