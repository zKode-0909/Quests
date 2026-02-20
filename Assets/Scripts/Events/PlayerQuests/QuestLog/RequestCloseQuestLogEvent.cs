using UnityEngine;

public struct RequestCloseQuestLogEvent : IEvent
{
    public int EntityRuntimeID;

    public RequestCloseQuestLogEvent(int entityID) { 
        EntityRuntimeID = entityID;
    }
}
