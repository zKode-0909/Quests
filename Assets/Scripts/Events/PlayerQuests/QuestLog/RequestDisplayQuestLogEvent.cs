using System.Collections.Generic;
using UnityEngine;

public struct RequestDisplayQuestLogEvent : IEvent
{
    public int EntityRuntimeID;

    public RequestDisplayQuestLogEvent(int entityID) {
        EntityRuntimeID = entityID;
    }
}
