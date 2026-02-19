using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestSettings", menuName = "Quests/BaseQuestSettings")]
public class QuestSettings : ScriptableObject
{
    public string ID;
    public string QuestName;
    public string QuestDescription;
    public int RequiredLevel;
    public List<QuestSettings> preReqQuests;

    public Quest CreateQuest(QuestGiver questGiver) {
        return new Quest(QuestName,questGiver.questGiverID,ID);
    }
    public Quest CreateQuest(string questGiverID)
    {

        return new Quest(QuestName, questGiverID,ID);
    }

}
