using System.Collections.Generic;
using UnityEngine;

public sealed class QuestLogRegistry
{
    EventBinding<RegisterQuestLogEvent> registerQuestLogBinding;

    private readonly Dictionary<string, QuestLog> _logs = new();

    public IEnumerable<KeyValuePair<string, QuestLog>> All => _logs;

    public bool TryGet(string id, out QuestLog log) => _logs.TryGetValue(id, out log);

    public void InitializeRegistry() {
        registerQuestLogBinding = new EventBinding<RegisterQuestLogEvent>(RegisterQuestLog);
        EventBus<RegisterQuestLogEvent>.Register(registerQuestLogBinding);
    }

    public void RegisterQuestLog(RegisterQuestLogEvent evt) {
        GetOrCreate(evt.EntityStableID,evt.humanLog);
    }

    public QuestLog GetOrCreate(string id,bool human)
    {
        if (_logs.TryGetValue(id, out var log)) return log;
        log = new QuestLog(25,human);
        _logs.Add(id, log);
        return log;
    }

    public bool Remove(string id) => _logs.Remove(id);

    public bool TryCreate(string id,bool human) {
        if (_logs.TryGetValue(id, out var log)) { 
            return false;
        }

        log = new QuestLog(25, human);
        _logs.Add(id, log);
        return true;

    }
}
