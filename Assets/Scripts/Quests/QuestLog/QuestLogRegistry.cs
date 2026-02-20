using System.Collections.Generic;
using UnityEngine;

public sealed class QuestLogRegistry : IQuestLogRegistry
{
    

    private readonly Dictionary<EntityId, QuestLog> _logs = new();

    public IEnumerable<KeyValuePair<EntityId, QuestLog>> All => _logs;

    public bool TryGet(EntityId id, out QuestLog log) => _logs.TryGetValue(id, out log);

    public QuestLog GetOrCreate(EntityId id)
    {
        if (_logs.TryGetValue(id, out var log)) return log;
        log = new QuestLog(id);
        _logs.Add(id, log);
        return log;
    }

    public bool Remove(EntityId id) => _logs.Remove(id);

    public bool TryCreate(EntityId id) {
        if (_logs.TryGetValue(id, out var log)) { 
            return false;
        }

        log = new QuestLog(25);
        _logs.Add(id, log);
        return true;

    }
}
