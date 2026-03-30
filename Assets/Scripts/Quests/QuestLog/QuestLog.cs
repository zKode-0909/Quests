using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class QuestLog
{
    public Dictionary<string,Quest> quests;
    public HashSet<string> completedQuests;
    public int capacity;
    public bool humanLog;
 


    public QuestLog(int capacity, bool humanLog)
    {
        this.capacity = capacity;
        quests = new Dictionary<string, Quest>();
        completedQuests = new HashSet<string>();
        this.humanLog = humanLog;
    }

    public void SetCurrentAndCompletedQuests(List<Quest> quests,List<string> completed) {
        foreach (Quest quest in quests) {
            quests.Add(quest);
        }

        foreach (string completedQuest in completed) {
            completedQuests.Add(completedQuest);
        }
    }


    /*
    public Quest GetQuestByObjective() { 
        return 
    }*/

    public Dictionary<string,Quest> GetQuests() { 
        
        return quests;
    }

    public List<string> GetCompletedQuests() { 
        return completedQuests.ToList();
    }



    public bool TryAddQuest(Quest quest) {
        if (quests.Count >= capacity-1) {
            Debug.Log("log full");
            return false;
        }



        if (quest == null || string.IsNullOrEmpty(quest.questID)) {
            return false;

        }

        
        var added = quests.TryAdd(quest.questID, quest);
        if (!added) {
            return false; // already exists
        }

        
        

        return true;
    }


    public void TryIncrementQuestObjective(string objectiveID) {
        foreach (KeyValuePair<string, Quest> quest in quests) {
            quest.Value.OnObjectiveEvent(objectiveID);
        }
    }



    

    public void RemoveQuest(Quest quest) { quests.Remove(quest.questID); }
    public void RemoveQuest(string questID) { quests.Remove(questID); }
}
