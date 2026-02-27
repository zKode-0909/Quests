using UnityEngine;

public struct EntityWorldQuestStateChangedEvent : IEvent
{
    public int EntityRuntimeID;
    public string QuestID;

    public EntityWorldQuestStateChangedEvent(int entityID,string questID) { 
        EntityRuntimeID = entityID;
        QuestID = questID;
    }
}
