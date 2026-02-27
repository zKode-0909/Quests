using JetBrains.Annotations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public int entityOwnerRuntimeID;
    public string questName;
    public string questGiverID;
    public string questID;
    public int questRuntimeID;
    public int questLevel;
    public event Action<int,string> allObjectiveCompleteEvent;
    public QuestObjective questObjectives;
    private Dictionary<string, int> progressByStableId;
    int stageIndex = 0;
    bool complete;

    public Quest(string name,string questGiverID,string questID,int runtimeID,int questLevel,QuestObjective objectives,int entityID)
    {
        this.questName = name;
        this.questGiverID = questGiverID;
        this.questID = questID;
        this.questRuntimeID = runtimeID;
        this.questLevel = questLevel;
        this.questObjectives = objectives;
        this.entityOwnerRuntimeID = entityID;

        progressByStableId = new Dictionary<string, int>();

        // initialize progress for each requirement
        foreach (var stage in questObjectives.Stages)
        {
            
            foreach (var obj in stage.requirements)
            {
                string idKey = obj.requirement.GetQuestObjectiveStableID();

                progressByStableId[idKey] = 0;
            }
        }
    }

    public bool GetQuestCompletionStatus() { 
        return complete;
    }


    public void OnObjectiveEvent(string objectiveThingId)
    {
        if (progressByStableId.TryGetValue(objectiveThingId, out var count))
        {
            if (questObjectives.TryGetMatchingRequirementId(stageIndex, objectiveThingId, out var stableId))
                progressByStableId[stableId]++;




            if (stageIndex <= questObjectives.Stages.Count - 1 && questObjectives.IsStageComplete(stageIndex, progressByStableId))
                stageIndex++;


            if (stageIndex == questObjectives.Stages.Count)
            {
                Debug.Log($"you have completed the quest!");
                complete = true;
                allObjectiveCompleteEvent?.Invoke(entityOwnerRuntimeID,questID);
            }
        }
        else {
            return;
        }
        

        
        

    }
}
