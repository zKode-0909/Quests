using UnityEngine;

public struct RegisterQuestLogEvent : IEvent
{
    public int EntityRuntimeID;

    public RegisterQuestLogEvent(int entityID)
    {
        EntityRuntimeID = entityID;
    }
}
