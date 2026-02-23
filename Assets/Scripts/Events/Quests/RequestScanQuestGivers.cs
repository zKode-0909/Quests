using UnityEngine;

public struct RequestScanQuestGivers : IEvent
{
    public int EntityRuntimeID;
    public int EntityLevel;

    public RequestScanQuestGivers(int runtimeID,int level) { 
        EntityRuntimeID = runtimeID;
        EntityLevel = level;
    }

}
