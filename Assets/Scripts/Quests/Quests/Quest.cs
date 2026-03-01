using JetBrains.Annotations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    int entityOwnerRuntimeID;
    string questName;
    string questGiverID;
    string questID;
    int questRuntimeID;
    int questLevel;
    public event Action<int,string> allObjectiveCompleteEvent;
    QuestStages questStages;

    public Quest(string name,string questGiverID,string questID,int runtimeID,int questLevel,QuestStages stages,int entityID)
    {
        this.questName = name;
        this.questGiverID = questGiverID;
        this.questID = questID;
        this.questRuntimeID = runtimeID;
        this.questLevel = questLevel;
        this.entityOwnerRuntimeID = entityID;
        this.questStages = stages;

        questStages.AllStagesCompleteEvent += HandleAllPhasesComplete;
    }

    public void HandleAllPhasesComplete() {
        allObjectiveCompleteEvent?.Invoke(entityOwnerRuntimeID, questID);
    }

    public bool GetQuestCompletionStatus() { 
        return questStages.GetQuestCompletionStatus();
    }


    public void OnObjectiveEvent(string objectiveThingId)
    {
        questStages.RequestIncrementObjective(objectiveThingId,1);
    }
}
