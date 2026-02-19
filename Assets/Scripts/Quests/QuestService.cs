using System;
using UnityEngine;

public class QuestService : MonoBehaviour
{
    public event Action<IQuester,Quest> QuestAcceptEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void TryAcceptQuest(IQuester quester, Quest quest) {
        if (quester.TryAddQuest(quest)) {
            Debug.Log($"succesfully added {quest.questName} to the quest log");
          
            return;
        }
        Debug.Log($"Quest log is full");
        
    }


}
