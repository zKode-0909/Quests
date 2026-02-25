using UnityEngine;

public class QuestLogController
{
    QuestFactory questFactory;
    private QuestLogRegistry logRegistry;
    private EventBinding<RequestDisplayQuestLogEvent> displayLogReqBinding;
    private EventBinding<RequestCloseQuestLogEvent> closeLogReqBinding;
    EventBinding<RequestAcceptQuest> acceptReqBinding;

    public void InitiateService(QuestFactory factory,QuestLogRegistry registry) {
        displayLogReqBinding = new EventBinding<RequestDisplayQuestLogEvent>(OnOpenQuestLogRequested);
        closeLogReqBinding = new EventBinding<RequestCloseQuestLogEvent>(OnCloseQuestLogRequested);
        

        logRegistry = registry;
        questFactory = factory;
        acceptReqBinding = new EventBinding<RequestAcceptQuest>(OnAcceptRequested);

        EventBus<RequestAcceptQuest>.Register(acceptReqBinding);
        EventBus<RequestDisplayQuestLogEvent>.Register(displayLogReqBinding);
        EventBus<RequestCloseQuestLogEvent>.Register(closeLogReqBinding);
    }

    public void Dispose() {
        EventBus<RequestDisplayQuestLogEvent>.Deregister(displayLogReqBinding);
        EventBus<RequestCloseQuestLogEvent>.Deregister(closeLogReqBinding);
        EventBus<RequestAcceptQuest>.Deregister(acceptReqBinding);

    }

    void OnAcceptRequested(RequestAcceptQuest evt)
    {


        Debug.Log($"player {evt.AccepterEntityRuntimeID} has succesfully accepted the quest");
        if (logRegistry.TryGet(evt.AccepterEntityRuntimeID, out var log))
        {
            if (questFactory.TryCreateQuestFromID(evt.QuestID, out var quest))
            {
                if (log.TryAddQuest(quest))
                {
                    EventBus<EntityAcceptQuest>.Raise(new EntityAcceptQuest(evt.AccepterEntityRuntimeID, evt.QuestID));
                }
            }
        }


    }

    void OnOpenQuestLogRequested(RequestDisplayQuestLogEvent evt)
    {


        if (logRegistry.TryGet(evt.EntityRuntimeID, out var log))
        {
            Debug.Log("Opening Quest Log");
            EventBus<DisplayQuestLogEvent>.Raise(new DisplayQuestLogEvent(questFactory.CreateQuestUIListFromQuestLog(log)));
        }
        else
        {
            Debug.LogWarning("failed opening quest log");
        }

    }


    void OnCloseQuestLogRequested()
    {
        EventBus<CloseQuestLogEvent>.Raise(new CloseQuestLogEvent());
    }

   


}
