
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestObjectiveSettings", menuName = "QuestSettings/QuestObjectiveSettings")]
public class QuestObjectiveSettings : ScriptableObject
{
    public List<QuestObjectiveDetails> questObjectives;

    public QuestObjective CreateRuntimeQuestObjective() {
        return new QuestObjective(questObjectives);
    }

    
}
