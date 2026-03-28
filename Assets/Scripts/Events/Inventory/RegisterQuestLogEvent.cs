using UnityEngine;

public struct RegisterQuestLogEvent : IEvent
{
    public string EntityStableID;

    public RegisterQuestLogEvent(string entityID)
    {
        EntityStableID = entityID;
    }
}
