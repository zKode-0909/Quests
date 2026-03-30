using System.Collections.Generic;
using UnityEngine;

public class QuestLogController
{
    QuestFactory questFactory;
    QuestActionRunner questActionRunner;
    private QuestLogRegistry logRegistry;
    private InventoryRegistry inventoryRegistry;
    private EventBinding<RequestDisplayQuestLogEvent> displayLogReqBinding;
    private EventBinding<RequestCloseQuestLogEvent> closeLogReqBinding;
    EventBinding<RequestAcceptQuest> acceptReqBinding;

    

    public void InitiateService(QuestFactory factory,QuestLogRegistry registry,QuestActionRunner actionRunner,InventoryRegistry inventoryRegistry) {
        displayLogReqBinding = new EventBinding<RequestDisplayQuestLogEvent>(OnOpenQuestLogRequested);
        closeLogReqBinding = new EventBinding<RequestCloseQuestLogEvent>(OnCloseQuestLogRequested);
        this.inventoryRegistry = inventoryRegistry;
        
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



    public void RequestIncrementQuestObjective(string questerStableID,string objectiveTriggerID) {
        if (logRegistry.TryGet(questerStableID, out var log)) {
            if (log.humanLog) {
                EventBus<IncrementPlayerQuestLogUI>.Raise(new IncrementPlayerQuestLogUI(objectiveTriggerID, 1));
            }
            log.TryIncrementQuestObjective(objectiveTriggerID);
        }
    }

    void OnAcceptRequested(RequestAcceptQuest evt)
    {
        if (logRegistry.TryGet(evt.AccepterEntityStableID, out var log))
        {
            if (questFactory.TryCreateQuestFromID(evt.QuestID,evt.AccepterEntityStableID,inventoryRegistry, out var quest))
            {
                if (log.TryAddQuest(quest))
                {
                    quest.allObjectiveCompleteEvent += HandleObjectivesComplete;
                    // quest.BindActionSink(actions => questActionRunner.HandleActions(actions));
                    quest.BindActions(questActionRunner);
                    EventBus<EntityWorldQuestStateChangedEvent>.Raise(new EntityWorldQuestStateChangedEvent(evt.AccepterEntityStableID, evt.QuestID));
                 
                }
            }
        }


    }

    void HandleObjectivesComplete(string questID,string stableID) {
        Debug.Log($"OBJECTIVES COMPLETE FOR {stableID} on questID: {questID}");
        EventBus<EntityWorldQuestStateChangedEvent>.Raise(new EntityWorldQuestStateChangedEvent(stableID, questID));
    }

    void OnOpenQuestLogRequested(RequestDisplayQuestLogEvent evt)
    {


        if (logRegistry.TryGet(evt.EntityStableID, out var log))
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
