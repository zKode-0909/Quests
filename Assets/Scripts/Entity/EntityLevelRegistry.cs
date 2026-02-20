using log4net.Core;
using System.Collections.Generic;
using UnityEngine;

public class EntityLevelRegistry
{
    private bool initialized;
    private Dictionary<int, int> entityLevelByID;

    EventBinding<EntityLevelChangedEvent> entityLevelChangedEvent;

    public void BuildRegistry() {
        if (initialized) return;
        initialized = true;
        entityLevelByID = new Dictionary<int, int>();
        entityLevelChangedEvent = new EventBinding<EntityLevelChangedEvent>(HandleEntityLevelChanged);
        EventBus<EntityLevelChangedEvent>.Register(entityLevelChangedEvent);
    }

    public void Dispose()
    {
        if (!initialized) return;
        initialized = false;

        EventBus<EntityLevelChangedEvent>.Deregister(entityLevelChangedEvent);
        entityLevelChangedEvent = null;
        entityLevelByID.Clear();
    }

    public void HandleEntityLevelChanged(EntityLevelChangedEvent evt) {
        if (entityLevelByID.TryGetValue(evt.EntityRuntimeID, out int level))
        {
            entityLevelByID[evt.EntityRuntimeID] = evt.NewEntityLevel;
        }
        else { 
            entityLevelByID.TryAdd(evt.EntityRuntimeID, evt.NewEntityLevel);
        }
    }

    public bool TryGetLevel(int entityId, out int level)
    => entityLevelByID.TryGetValue(entityId, out level);
}
