
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestSaveData
{
    [SerializeField] string questID;
    [SerializeField] string currentStage;
    [SerializeField] List<QuestStageRequirementContext> currentStageRequirements;

    public string QuestID => questID;
    public string CurrentStage => currentStage;
    public List<QuestStageRequirementContext> CurrentStageRequirements => currentStageRequirements;

    public QuestSaveData(string id,string stage,List<QuestStageRequirementContext> ctx) { 
        this.questID = id;
        this.currentStage = stage;
        this.currentStageRequirements = ctx;
    }
}
