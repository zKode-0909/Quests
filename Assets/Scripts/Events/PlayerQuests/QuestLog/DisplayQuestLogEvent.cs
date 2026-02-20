using System.Collections.Generic;
using UnityEngine;

public struct DisplayQuestLogEvent : IEvent
{
    public IReadOnlyList<QuestUIItem> Quests;

    public DisplayQuestLogEvent(IReadOnlyList<QuestUIItem> quests) { 
        Quests = quests;
    }
}
