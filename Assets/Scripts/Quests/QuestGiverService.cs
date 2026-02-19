using UnityEngine;

public static class QuestGiverService 
{
    private static EventBinding<RequestOpenQuestGiverUI> openReqBinding;

    public static void InitiateService()
    {
        openReqBinding = new EventBinding<RequestOpenQuestGiverUI>(OnOpenRequested);


        EventBus<RequestOpenQuestGiverUI>.Register(openReqBinding);
        
    }

    static void OnOpenRequested(RequestOpenQuestGiverUI evt) {
        if (EntityRegistry.TryGet(evt.InteractorEntityId, out var Interactor) && EntityRegistry.TryGet(evt.QuestGiverEntityId,out var Giver)) { 
            var interactor = (IInteractor)Interactor;
            var questGiver = Giver.GameObject.transform.GetComponent<QuestGiver>();

            if (interactor == null || questGiver == null) {
                Debug.Log("Missing interactor or questgiver");
            }

            var QuestItems = BuildQuestItems(QuestGiver questGiver)
        }
    }


    static void BuildQuestItems(QuestGiver questGiver) {
        foreach (var item in questGiver.Quests) { 
            var UIQuestItem = new QuestUIItem()
        }
    }
}
