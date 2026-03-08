using UnityEngine;

public struct RegisterCharacterEvent : IEvent
{
    public int EntityRuntimeID;

    public RegisterCharacterEvent(int entityID)
    {
        EntityRuntimeID = entityID;
    }
}
