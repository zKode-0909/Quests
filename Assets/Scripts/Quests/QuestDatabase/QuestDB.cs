
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestDB", menuName = "Scriptable Objects/QuestDB")]
public class QuestDB : ScriptableObject
{
    [SerializeField] List<QuestSettings> Quests = new();

    Dictionary<string,QuestSettings> QuestsByID;

    public void BuildQuestDB() {

        QuestsByID = new Dictionary<string,QuestSettings>(Quests.Count);

        foreach (var Quest in Quests)
        {
            if (Quest == null || string.IsNullOrWhiteSpace(Quest.ID)) continue;

            if (QuestsByID.ContainsKey(Quest.ID))
                Debug.LogError($"Duplicate QuestId: {Quest.ID}");
            else
                QuestsByID.Add(Quest.ID, Quest);
        }
    }

    public bool TryGetQuestDef(string questID,out QuestSettings quest) {
        if (QuestsByID == null) { 
            BuildQuestDB();
        }
        if (QuestsByID.TryGetValue(questID, out var q))
        {
            quest = q;
            return true;
        }
        else { 
            quest = null;
            return false;
        }
    }
}
