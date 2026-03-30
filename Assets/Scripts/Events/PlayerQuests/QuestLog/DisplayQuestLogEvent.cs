using System.Collections.Generic;
using UnityEngine;

public struct DisplayQuestLogEvent : IEvent
{
    public List<QuestUIItem> Quests;

    public DisplayQuestLogEvent(List<QuestUIItem> quests) { 
        Quests = quests;
    }
}
