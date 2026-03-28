using UnityEngine;

public struct RequestOpenQuestGiverUI : IEvent
{
    public readonly string QuesterEntityId;
    public readonly string QuestGiverEntityId;
    public readonly int QuesterLevel;
    public RequestOpenQuestGiverUI(string questerEntityId, string questGiverEntityId,int questerLevel)
    {
        QuesterEntityId = questerEntityId;
        QuestGiverEntityId = questGiverEntityId;
        QuesterLevel = questerLevel;
    }
}
