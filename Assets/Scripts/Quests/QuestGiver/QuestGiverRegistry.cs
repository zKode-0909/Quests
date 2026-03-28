using System.Collections.Generic;
using UnityEngine;

public class QuestGiverRegistry
{
    Dictionary<string, QuestGiver> questGivers;

    public QuestGiverRegistry() { 
        questGivers = new Dictionary<string, QuestGiver>();
    }

    public void Dispose() { 
        questGivers.Clear();
    }

    public bool TryRegister(QuestGiver questGiver) {

        if (questGiver == null) return false;
        
        return questGivers.TryAdd(questGiver.StableID, questGiver);
    }


    public bool TryUnregister(string entityId)
        => questGivers.Remove(entityId);

    public bool TryGet(string entityId, out QuestGiver questGiver)
        => questGivers.TryGetValue(entityId, out questGiver);





}
