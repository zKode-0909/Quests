using System.Collections.Generic;
using UnityEngine;

public class QuestLogController
{
    QuestFactory questFactory;
    QuestActionRunner questActionRunner;
    private QuestLogRegistry logRegistry;
    private EventBinding<RequestDisplayQuestLogEvent> displayLogReqBinding;
    private EventBinding<RequestCloseQuestLogEvent> closeLogReqBinding;
    EventBinding<RequestAcceptQuest> acceptReqBinding;

    public void InitiateService(QuestFactory factory,QuestLogRegistry registry,QuestActionRunner actionRunner) {
        displayLogReqBinding = new EventBinding<RequestDisplayQuestLogEvent>(OnOpenQuestLogRequested);
        closeLogReqBinding = new EventBinding<RequestCloseQuestLogEvent>(OnCloseQuestLogRequested);
        
        questActionRunner = actionRunner;
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



    public void RequestIncrementQuestObjective(int questerRuntimeID,string objectiveTriggerID) {
        if (logRegistry.TryGet(questerRuntimeID, out var log)) {
            log.TryIncrementQuestObjective(objectiveTriggerID);
        }
    }

    void OnAcceptRequested(RequestAcceptQuest evt)
    {
        if (logRegistry.TryGet(evt.AccepterEntityRuntimeID, out var log))
        {
            if (questFactory.TryCreateQuestFromID(evt.QuestID,evt.AccepterEntityRuntimeID, out var quest))
            {
                if (log.TryAddQuest(quest))
                {
                    quest.allObjectiveCompleteEvent += HandleObjectivesComplete;
                    // quest.BindActionSink(actions => questActionRunner.HandleActions(actions));
                    quest.BindActions(questActionRunner);
                    EventBus<EntityWorldQuestStateChangedEvent>.Raise(new EntityWorldQuestStateChangedEvent(evt.AccepterEntityRuntimeID, evt.QuestID));
                    foreach (KeyValuePair<string,Quest> pair in log.GetQuests()) {
                    }
                }
            }
        }


    }

    void HandleObjectivesComplete(string questID,int runtimeID) {
        Debug.Log($"OBJECTIVES COMPLETE FOR {runtimeID} on questID: {questID}");
        EventBus<EntityWorldQuestStateChangedEvent>.Raise(new EntityWorldQuestStateChangedEvent(runtimeID, questID));
    }

    void OnOpenQuestLogRequested(RequestDisplayQuestLogEvent evt)
    {


        if (logRegistry.TryGet(evt.EntityRuntimeID, out var log))
        {
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
