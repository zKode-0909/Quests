using System.Collections.Generic;
using UnityEngine;

public static class QuestGiverService 
{
    private static EventBinding<RequestOpenQuestGiverUI> openReqBinding;
    private static EventBinding<RefreshQuestGiverUI> refreshReqBinding;
    private static EventBinding<RequestAcceptQuest> acceptReqBinding;

    public static void InitiateService()
    {
        openReqBinding = new EventBinding<RequestOpenQuestGiverUI>(OnOpenRequested);
        Debug.Log("service intiated");

        EventBus<RequestOpenQuestGiverUI>.Register(openReqBinding);
        EventBus<RefreshQuestGiverUI>.Register(refreshReqBinding);
        EventBus<RequestAcceptQuest>.Register(acceptReqBinding);
        
    }

    static void OnOpenRequested(RequestOpenQuestGiverUI evt) {
        Debug.Log("requested open");

        if (!EntityRegistry.TryGet(evt.InteractorEntityId, out var interactorEntity))
        {
            Debug.LogWarning($"Interactor entity not found: {evt.InteractorEntityId}");
            return;
        }

        if (!EntityRegistry.TryGet(evt.QuestGiverEntityId, out var giverEntity))
        {
            Debug.LogWarning($"QuestGiver entity not found: {evt.QuestGiverEntityId}");
            return;
        }

        if (interactorEntity is not IQuester quester)
        {
            Debug.LogWarning($"Entity {evt.InteractorEntityId} does not implement IQuester");
            return;
        }

        if (giverEntity is not IQuestGiver questGiver)
        {
            Debug.LogWarning($"QuestGiver component missing on {giverEntity.GameObject.name}");
            return;
        }

        var questItems = BuildQuestItems(questGiver, quester);

        EventBus<OpenQuestGiverUI>.Raise(new OpenQuestGiverUI(questItems,"TestGiverName","TestQuesterName",questGiver.EntityRuntimeID,quester.EntityRuntimeID));
        Debug.Log("Raised event");
    }

    static void OnAcceptRequested(RequestAcceptQuest evt) {
        if (!EntityRegistry.TryGet(evt.AccepterEntityRuntimeID, out var accepter))
        {
            Debug.LogWarning($"Accepter entity not found: {evt.AccepterEntityRuntimeID}");
            return;
        }

        if (!EntityRegistry.TryGet(evt.GiverEntityRuntimeID, out var giver))
        {
            Debug.LogWarning($"QuestGiver entity not found: {evt.GiverEntityRuntimeID}");
            return;
        }

        if (accepter is not IQuester quester)
        {
            Debug.LogWarning($"Entity {accepter} does not implement IQuester");
            return;
        }

        if (giver is not IQuestGiver questGiver)
        {
            Debug.LogWarning($"QuestGiver component missing on {giver}");
            return;
        }

        Debug.Log($"player {accepter.EntityRuntimeID} has succesfully accepted the quest");

    }


    static List<QuestUIItem> BuildQuestItems(IQuestGiver questGiver,IQuester quester) {

        var items = new List<QuestUIItem>();    
        foreach (var quest in questGiver.Quests) {
            var status = QuestUtils.DetermineQuestStatus(quest, quester);
            if (status != QuestStatus.None) {
                items.Add(new QuestUIItem(quest.QuestName, quest.QuestDescription, questGiver.EntityRuntimeID, quest.ID, status));
            }
        }

        return items;
    }
}
