using System.Collections.Generic;
using UnityEngine;

public class QuestGiverRegistry
{
    Dictionary<int, QuestGiver> questGivers;

    public QuestGiverRegistry() { 
        questGivers = new Dictionary<int, QuestGiver>();
    }

    public void Dispose() { 
        questGivers.Clear();
    }

    public bool TryRegister(QuestGiver questGiver) {

        if (questGiver == null) return false;
        
        return questGivers.TryAdd(questGiver.entityRuntimeId, questGiver);
    }


    public bool TryUnregister(int entityId)
        => questGivers.Remove(entityId);

    public bool TryGet(int entityId, out QuestGiver questGiver)
        => questGivers.TryGetValue(entityId, out questGiver);





}
