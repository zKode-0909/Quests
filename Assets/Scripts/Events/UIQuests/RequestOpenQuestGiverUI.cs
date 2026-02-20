using UnityEngine;

public struct RequestOpenQuestGiverUI : IEvent
{
    public readonly int QuesterEntityId;
    public readonly int QuestGiverEntityId;
    public readonly int QuesterLevel;
    public RequestOpenQuestGiverUI(int questerEntityId, int questGiverEntityId,int questerLevel)
    {
        QuesterEntityId = questerEntityId;
        QuestGiverEntityId = questGiverEntityId;
        QuesterLevel = questerLevel;
    }
}
