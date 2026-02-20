using System.Collections.Generic;
using UnityEngine;

public static class QuestUtils
{
    public static QuestDisplayIcon DetermineIcon(QuestGiver questGiver,QuestLog questLog,int level) {
        var displayIcons = new List<QuestDisplayIcon>();
        foreach (var quest in questGiver.Quests) {
            if (quest.RequiredLevel <= level) {
                if (questLog.quests.ContainsKey(quest.ID))
                {
                    displayIcons.Add(QuestDisplayIcon.InProgress);
                }
                else { 
                    displayIcons.Add(QuestDisplayIcon.Available);
                }
            }
        }

        if (displayIcons.Contains(QuestDisplayIcon.Available))
        {
            return QuestDisplayIcon.Available;
        }
        else if (displayIcons.Contains(QuestDisplayIcon.Completed))
        {
            return QuestDisplayIcon.Completed;
        }
        else if (displayIcons.Contains(QuestDisplayIcon.InProgress))
        {
            return QuestDisplayIcon.InProgress;
        }
        else { 
            return QuestDisplayIcon.None;
        }
        
    }


    public static QuestStatus DetermineQuestStatus(QuestSettings quest,QuestLog questLog,int level) {
        if (questLog.GetQuests().ContainsKey(quest.ID)) { 
            return QuestStatus.InProgress;
        }
        //TODO: completed condition
        else if ((!questLog.completedQuests.Contains(quest.ID) && (quest.preReqQuests.Count == 0 || CheckPreReqs(quest,questLog.completedQuests))
            && !questLog.GetQuests().ContainsKey(quest.ID) && level >= quest.RequiredLevel)){ 
            return QuestStatus.NotStarted;
        }
        Debug.Log($"Returning none: contains quest already?: {questLog.GetQuests().ContainsKey(quest.ID)} required level: {quest.RequiredLevel}  quester level: {level}  completed quest already?: {questLog.completedQuests.Contains(quest.ID)} prereqs?: {quest.preReqQuests.Count == 0 || CheckPreReqs(quest,questLog.completedQuests)}");
        return QuestStatus.None;
        
    }

    public static QuestStatus DetermineQuestRuntimeStatus(Quest quest)
    {
        
        return QuestStatus.InProgress;
        

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


