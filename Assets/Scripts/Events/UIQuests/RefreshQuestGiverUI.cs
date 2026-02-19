using System.Collections.Generic;
using UnityEngine;

public struct RefreshQuestGiverUI : IEvent
{
    public int questerRuntimeID;
    public int questGiverRuntimeID;
    public IReadOnlyList<QuestUIItem> quests;

    public RefreshQuestGiverUI(int questerID,int giverID,IReadOnlyList<QuestUIItem> quests) { 
        this.questerRuntimeID = questerID;
        this.questGiverRuntimeID = giverID;
        this.quests = quests;
    }
}
