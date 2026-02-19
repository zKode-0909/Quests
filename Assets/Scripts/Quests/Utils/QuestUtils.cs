using System.Collections.Generic;
using UnityEngine;

public static class QuestUtils
{
    public static QuestDisplayIcon DetermineIcon(QuestGiver questGiver,IQuester quester) {
        var playerLevel = quester.QuesterLevel;
        var playerQuestLog = quester.QuestLog;
        foreach (var quest in questGiver.Quests) {
            if (quest.RequiredLevel <= playerLevel) {
                if (playerQuestLog.quests.ContainsKey(quest.ID))
                {
                    return QuestDisplayIcon.InProgress;
                }
                else { 
                    return QuestDisplayIcon.Available;
                }
            }
        }

        return QuestDisplayIcon.None;
        
    }


    public static QuestStatus DetermineQuestStatus(QuestSettings quest,IQuester quester) {
        if (quester.QuestLog.GetQuests().ContainsKey(quest.ID)) { 
            return QuestStatus.InProgress;
        }
        //TODO: completed condition
        else if ((!quester.CompletedQuests.Contains(quest.ID) && (quest.preReqQuests.Count == 0 || CheckPreReqs(quest,quester.CompletedQuests))
            && !quester.QuestLog.GetQuests().ContainsKey(quest.ID) &&quester.QuesterLevel >= quest.RequiredLevel)){ 
            return QuestStatus.NotStarted;
        }

        return QuestStatus.None;
        
    }

    public static bool CheckPreReqs(QuestSettings quest,HashSet<string> completedQuests) {
        foreach (var q in quest.preReqQuests) {
            if (!completedQuests.Contains(q.ID)) { 
                return false;
            }
        }

        return true;
    }


}


