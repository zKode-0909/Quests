using UnityEngine;
using System.Collections.Generic;

public class QuestLog
{
    public Dictionary<string,Quest> quests;
    public HashSet<string> completedQuests;
    public int capacity;
    bool open;


    public QuestLog(int capacity)
    {
        this.capacity = capacity;
        quests = new Dictionary<string, Quest>();
        completedQuests = new HashSet<string>();
        Debug.Log($"Questlog create with {capacity} elements");
    }

    public Dictionary<string,Quest> GetQuests() { 
        
        return quests;
    }



    public bool TryAddQuest(Quest quest) {
        Debug.Log($"quest: {quest}");
        if (quests.Count >= capacity-1) {
            Debug.Log("log full");
            return false;
        }



        if (quest == null || string.IsNullOrEmpty(quest.questID)) {
            Debug.Log("Invalid quest");
            return false;

        }

        
        var added = quests.TryAdd(quest.questID, quest);
        if (!added) {
            Debug.Log("already added");
            return false; // already exists
        }

        
        

        return true;
    }



    

    public void RemoveQuest(Quest quest) { quests.Remove(quest.questID); }
    public void RemoveQuest(string questID) { quests.Remove(questID); }
}
