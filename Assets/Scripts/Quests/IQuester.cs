using System.Collections.Generic;
using UnityEngine;

public interface IQuester
{
    public QuestLog QuestLog { get; }
    public HashSet<string> CompletedQuests { get; }
    public int QuesterLevel { get; }
    public bool TryAddQuest(Quest quest);

    public int EntityRuntimeID { get; }
    //public void HandleAcceptQuest(IQuester quester,Quest quest);
}
