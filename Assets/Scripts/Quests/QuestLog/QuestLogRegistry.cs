using System.Collections.Generic;
using UnityEngine;

public sealed class QuestLogRegistry
{
    EventBinding<RegisterQuestLogEvent> registerQuestLogBinding;

    private readonly Dictionary<int, QuestLog> _logs = new();

    public IEnumerable<KeyValuePair<int, QuestLog>> All => _logs;

    public bool TryGet(int id, out QuestLog log) => _logs.TryGetValue(id, out log);

    public void InitializeRegistry() {
        registerQuestLogBinding = new EventBinding<RegisterQuestLogEvent>(RegisterQuestLog);
        EventBus<RegisterQuestLogEvent>.Register(registerQuestLogBinding);
    }

    public void RegisterQuestLog(RegisterQuestLogEvent evt) {
        GetOrCreate(evt.EntityRuntimeID);
    }

    public QuestLog GetOrCreate(int id)
    {
        if (_logs.TryGetValue(id, out var log)) return log;
        log = new QuestLog(25);
        _logs.Add(id, log);
        return log;
    }

    public bool Remove(int id) => _logs.Remove(id);

    public bool TryCreate(int id) {
        if (_logs.TryGetValue(id, out var log)) { 
            return false;
        }

        log = new QuestLog(25);
        _logs.Add(id, log);
        return true;

    }
}
