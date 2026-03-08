using UnityEngine;

public struct RegisterPlayerEvent : IEvent
{
    public int EntityRuntimeID;
   

    public RegisterPlayerEvent(int entityID)
    {
        EntityRuntimeID = entityID;
    }
}
