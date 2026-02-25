using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string questName;
    public string questGiverID;
    public string questID;
    public int questRuntimeID;
    public int questLevel;
    public List<QuestObjective> questObjectives;

    public Quest(string name,string questGiverID,string questID,int runtimeID,int questLevel,List<QuestObjective> objectives)
    {
        this.questName = name;
        this.questGiverID = questGiverID;
        this.questID = questID;
        this.questRuntimeID = runtimeID;
        this.questLevel = questLevel;
        this.questObjectives = objectives;
    }   
}
