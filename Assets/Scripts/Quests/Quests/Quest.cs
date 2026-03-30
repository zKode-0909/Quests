using JetBrains.Annotations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string entityOwnerStableID { get; private set; }
    public string questName { get; private set; }
    public string questGiverID { get; private set; }
    public string questID { get; private set; }
    public int questRuntimeID { get; private set; }
    public int questLevel { get; private set; }
    public event Action<string,string> allObjectiveCompleteEvent;
    QuestStages questStages;
    private Action<IReadOnlyList<QuestAction>> actionSink;

    Dictionary<string, int> progressByTargetId;

    Inventory currentPlayerInventory;

    public Quest(string name,string questGiverID,string questID,int runtimeID,int questLevel,QuestStages stages,string entityID,Inventory inventory)
    {
        this.questName = name;
        this.questGiverID = questGiverID;
        this.questID = questID;
        this.questRuntimeID = runtimeID;
        this.questLevel = questLevel;
        this.entityOwnerStableID = entityID;
        this.questStages = stages;

        currentPlayerInventory = inventory;

        questStages.SetInventory(inventory);

        progressByTargetId = new Dictionary<string, int>();

        questStages.QuestObjectiveEvent += HandleAction;
        questStages.ObjectivesFinishedEvent += HandleObjectivesComplete;
        questStages.SetTargetIDDict(progressByTargetId);

        foreach (var item in currentPlayerInventory.GetItems())
        {
            foreach (var requirement in questStages.GetCurrentStage().GetStageRequirements())
            {
                if (item != null) {
                    Debug.Log($"checking item: {item.StableID} vs objective {requirement.Value.GetQuestObjectiveStableID()}");
                    if (requirement.Value.GetQuestObjectiveStableID() == item.StableID)
                    {
                        OnObjectiveEvent(item.StableID);
                    }
                }
                
                
            }
        }

    }

    public void BindActions(QuestActionRunner runner)
    {
        actionSink += runner.HandleActions;
    }

    private void EmitActions(IReadOnlyList<QuestAction> actions)
    {

        foreach (QuestAction action in actions) {
            Debug.Log($"emitting action {action}");
            action.Execute();
        }
       // actionSink?.Invoke(actions);
    }

    void HandleObjectivesComplete() {
        allObjectiveCompleteEvent?.Invoke(questID,entityOwnerStableID);
    }



    public void HandleAction(IReadOnlyList<QuestAction> actions) {
        EmitActions(actions);
        //allObjectiveCompleteEvent?.Invoke(entityOwnerRuntimeID, questID);
    }

    public bool GetQuestCompletionStatus() { 
        return questStages.GetQuestCompletionStatus();
    }


    public void OnObjectiveEvent(string objectiveThingId)
    {
        if (!progressByTargetId.TryGetValue(objectiveThingId, out var progress)) {
            progressByTargetId.Add(objectiveThingId, 0);
        }
        questStages.RequestIncrementObjective(objectiveThingId,1);
    }

    public QuestStages GetQuestStages() { 
        return questStages;
    }
}
