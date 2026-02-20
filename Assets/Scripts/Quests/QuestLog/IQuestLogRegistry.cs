using System.Collections.Generic;
using UnityEngine;


    public interface IQuestLogRegistry
    {
        bool TryGet(EntityId id, out QuestLog log);

        /// Returns an existing log or creates one (common in gameplay code).
        QuestLog GetOrCreate(EntityId id);

        /// Explicit lifetime control.
        bool Remove(EntityId id);

        /// Optional: query (UI/debug)
        IEnumerable<KeyValuePair<EntityId, QuestLog>> All { get; }
    }

