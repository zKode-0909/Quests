using JetBrains.Annotations;
using UnityEngine;

public class Quest
{
    public string questName;
    public string questGiverID;
    public string questID;
    public int questRuntimeID;
    public int questLevel;

    public Quest(string name,string questGiverID,string questID,int runtimeID,int questLevel)
    {
        this.questName = name;
        this.questGiverID = questGiverID;
        this.questID = questID;
        this.questRuntimeID = runtimeID;
        this.questLevel = questLevel;
    }   
}
