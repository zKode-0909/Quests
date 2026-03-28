using System.Collections.Generic;

using UnityEngine;

public class QuestFactory
{
    QuestDB dataBase;
    

    public void InitializeFactory(QuestDB db) {
        dataBase = db;
        

    }

    public bool TryCreateQuestFromID(string questID,string receiverStableID, out Quest quest) {
        if (dataBase.TryGetQuestDef(questID, out var q))
        {
            var QuestSettings = q;
            quest = new Quest(QuestSettings.QuestName, "blank", QuestSettings.ID, 3, QuestSettings.RequiredLevel,QuestSettings.objectives.BuildRuntimeQuestStages(),receiverStableID);
            return true;
        }
        else {
            quest = null;
            return false;
        }

        
    }



    public QuestUIItem CreateQuestUIFromQuest(Quest runtimeQuest) { 
        var status = QuestUtils.DetermineQuestStatus(runtimeQuest);
        return new QuestUIItem(runtimeQuest.questName, "Sorry forgot", 2, runtimeQuest.questID,$"{status}");

        
    }

    public IReadOnlyList<QuestUIItem> CreateQuestUIListFromQuestLog(QuestLog log) {
        var uiQuests = new List<QuestUIItem>();

        foreach (KeyValuePair<string, Quest> quest in log.GetQuests())
        {
            var status = QuestUtils.DetermineQuestStatus(quest.Value);
            var item = new QuestUIItem(quest.Value.questName, "FAKE DESCRIPTION", 2, quest.Value.questGiverID, $"{status}");

            uiQuests.Add(item);
        }

        return uiQuests;
    }
}
