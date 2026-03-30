
using System.Collections.Generic;
using UnityEngine;

public class QuestUIItem 
{
    public readonly string title;
    public readonly string description;
    public readonly int questGiverID;
    public readonly string questID;
    public readonly string status;
    public List<QuestRequirementProgress> objectives;
    

    public QuestUIItem(string title,string description,int giverID,string questID,string status,List<QuestRequirementProgress> objectives) { 
        this.title = title;
        this.description = description;
        this.questGiverID = giverID;
        this.questID = questID;
        this.status = status;
        this.objectives = objectives;

    }

}
