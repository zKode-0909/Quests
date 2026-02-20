using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class QuestFactory
{
    QuestDB dataBase;
    

    public void InitializeFactory(QuestDB db) {
        dataBase = db;
   

    }

    public bool TryCreateQuestFromID(string questID,out Quest quest) {
        if (dataBase.TryGetQuestDef(questID, out var q))
        {
            var QuestSettings = q;
            quest = new Quest(QuestSettings.QuestName, "blank", QuestSettings.ID, 3, QuestSettings.RequiredLevel);
            return true;
        }
        else {
            quest = null;
            return false;
        }

        
    }

    public QuestUIItem CreateQuestUIFromQuest(Quest runtimeQuest) { 
        var status = QuestUtils.DetermineQuestRuntimeStatus(runtimeQuest);
        return new QuestUIItem(runtimeQuest.questName,"Sorry forgot",2,runtimeQuest.questID,status);

        
    }

    public IReadOnlyList<QuestUIItem> CreateQuestUIListFromQuestLog(QuestLog log) {
        var uiQuests = new List<QuestUIItem>();

        foreach (KeyValuePair<string, Quest> quest in log.GetQuests())
        {
            var status = QuestUtils.DetermineQuestRuntimeStatus(quest.Value);
            var item = new QuestUIItem(quest.Value.questName, "FAKE DESCRIPTION", 2, quest.Value.questGiverID, status);

            uiQuests.Add(item);
        }

        return uiQuests;
    }
}
