using UnityEngine;

public class QuestLogController
{
    QuestFactory questFactory;
    private QuestLogRegistry logRegistry;
    private EventBinding<RequestDisplayQuestLogEvent> displayLogReqBinding;
    private EventBinding<RequestCloseQuestLogEvent> closeLogReqBinding;

    public void InitiateService(QuestFactory factory,QuestLogRegistry registry) {
        displayLogReqBinding = new EventBinding<RequestDisplayQuestLogEvent>(OnOpenQuestLogRequested);
        closeLogReqBinding = new EventBinding<RequestCloseQuestLogEvent>(OnCloseQuestLogRequested);
        

        logRegistry = registry;
        questFactory = factory;

        EventBus<RequestDisplayQuestLogEvent>.Register(displayLogReqBinding);
        EventBus<RequestCloseQuestLogEvent>.Register(closeLogReqBinding);
    }

    public void Dispose() {
        EventBus<RequestDisplayQuestLogEvent>.Deregister(displayLogReqBinding);
        EventBus<RequestCloseQuestLogEvent>.Deregister(closeLogReqBinding);

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
