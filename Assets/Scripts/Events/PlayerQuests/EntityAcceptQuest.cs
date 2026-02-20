using UnityEngine;

public struct EntityAcceptQuest : IEvent
{
    public int EntityRuntimeID;
    public string QuestID;

    public EntityAcceptQuest(int entityID,string questID) { 
        EntityRuntimeID = entityID;
        QuestID = questID;
    }
}
