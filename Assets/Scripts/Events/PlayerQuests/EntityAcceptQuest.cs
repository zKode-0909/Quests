using UnityEngine;

public struct EntityWorldQuestStateChangedEvent : IEvent
{
    public string EntityStableID;
    public string QuestID;

    public EntityWorldQuestStateChangedEvent(string entityID,string questID) { 
        EntityStableID = entityID;
        QuestID = questID;
    }
}
