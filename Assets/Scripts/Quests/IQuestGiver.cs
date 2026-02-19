using System.Collections.Generic;
using UnityEngine;

public interface IQuestGiver
{
    public int EntityRuntimeID { get; }
    public IReadOnlyList<QuestSettings> Quests { get; }
}
