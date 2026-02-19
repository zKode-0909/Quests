using UnityEngine;

public struct RequestOpenQuestGiverUI : IEvent
{
    public readonly int InteractorEntityId;
    public readonly int QuestGiverEntityId;
    public RequestOpenQuestGiverUI(int interactorEntityId, int questGiverEntityId)
    {
        InteractorEntityId = interactorEntityId;
        QuestGiverEntityId = questGiverEntityId;
    }
}
