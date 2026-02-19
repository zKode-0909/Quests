using UnityEngine;

public readonly struct QuestUIItem 
{
    readonly string title;
    readonly string description;
    readonly int questGiverID;
    readonly string questID;
    readonly QuestStatus status;

    public QuestUIItem(string title,string description,int giverID,string questID,QuestStatus status) { 
        this.title = title;
        this.description = description;
        this.questID = questID;
        this.status = status;

    }

}
