using System.Collections.Generic;

using UnityEngine;

public class QuestFactory
{
    QuestDB dataBase;
    

    public void InitializeFactory(QuestDB db) {
        dataBase = db;
        

    }

    public bool TryCreateQuestFromID(string questID,string receiverStableID,InventoryRegistry inventoryRegistry, out Quest quest) {
        if (dataBase.TryGetQuestDef(questID, out var q))
        {
            var QuestSettings = q;
            
            quest = new Quest(QuestSettings.QuestName, "blank", QuestSettings.ID, 3, QuestSettings.RequiredLevel,QuestSettings.objectives.BuildRuntimeQuestStages(),receiverStableID,inventoryRegistry.GetOrCreate(receiverStableID));
            return true;
        }
        else {
            quest = null;
            return false;
        }

        
    }



 

    public List<QuestUIItem> CreateQuestUIListFromQuestLog(QuestLog log) {
        var uiQuests = new List<QuestUIItem>();
        //List<List<QuestRequirementProgress>> objectives = new List<List<QuestRequirementProgress>>();
        foreach (KeyValuePair<string, Quest> quest in log.GetQuests())
        {
            List<QuestRequirementProgress> currentQuestReq = new List<QuestRequirementProgress>();
            foreach (var requirement in quest.Value.GetQuestStages().GetCurrentStage().GetStageRequirements())
            {
                currentQuestReq.Add(new QuestRequirementProgress(requirement.Key, requirement.Value.GetCurrentProgress(), requirement.Value.GetMaxProgressCount()));
            }
            //objectives.Add(currentQuestReq);
            var status = QuestUtils.DetermineQuestStatus(quest.Value);
            var item = new QuestUIItem(quest.Value.questName, "FAKE DESCRIPTION", 2, quest.Value.questID, $"{status}",currentQuestReq);
            //objectives.Clear();

            uiQuests.Add(item);
        }

        return uiQuests;
    }
}
