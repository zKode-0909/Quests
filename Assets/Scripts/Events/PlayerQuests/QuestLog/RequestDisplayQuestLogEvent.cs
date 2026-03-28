using System.Collections.Generic;
using UnityEngine;

public struct RequestDisplayQuestLogEvent : IEvent
{
    public string EntityStableID;

    public RequestDisplayQuestLogEvent(string entityID) {
        EntityStableID = entityID;
    }
}
