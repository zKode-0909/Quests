using UnityEngine;

public struct RegisterQuestLogEvent : IEvent
{
    public string EntityStableID;
    public bool humanLog;

    public RegisterQuestLogEvent(string entityID,bool humanLog)
    {
        EntityStableID = entityID;
        this.humanLog = humanLog;
    }
}
