using UnityEngine;

public struct EntityLevelChangedEvent : IEvent
{
    public int EntityRuntimeID;
    public int NewEntityLevel;

    public EntityLevelChangedEvent(int entityID,int level) {
        this.EntityRuntimeID = entityID;
        this.NewEntityLevel = level;
    }
}
