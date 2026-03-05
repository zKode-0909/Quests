using UnityEngine;

public class QuestService
{
    QuestLogController questLogController;
    private QuestLogRegistry logRegistry;
    QuestFactory questFactory;
    EventBinding<KilledEvent> killedEventBinding;
    EventBinding<GatherItemEvent> gatheredEventBinding;

    public void Initialize(QuestFactory factory, QuestLogRegistry registry,QuestLogController logController)
    {
        logRegistry = registry;
        questFactory = factory;
        questLogController = logController;    

        killedEventBinding = new EventBinding<KilledEvent>(HandleKilledEvent);
        gatheredEventBinding = new EventBinding<GatherItemEvent> (HandleGatheredEvent);

        EventBus<KilledEvent>.Register(killedEventBinding);
        EventBus<GatherItemEvent>.Register(gatheredEventBinding);
    }

    public void Dispose() {
        EventBus<KilledEvent>.Deregister(killedEventBinding);
        EventBus<GatherItemEvent>.Deregister(gatheredEventBinding);
    }

    void HandleGatheredEvent(GatherItemEvent evt) {
        questLogController.RequestIncrementQuestObjective(evt.gatheredByRuntimeID, evt.itemStableID);
    }


    void HandleKilledEvent(KilledEvent evt) {

        questLogController.RequestIncrementQuestObjective(evt.killedByRuntimeID, evt.killedCreatureStableID);

        /*
        Debug.Log($"the quest log that will need to be updated is...");
        if (logRegistry.TryGet(evt.killedByRuntimeID, out var log))
        {
            questLogController.RequestIncrementQuestObjective(log,evt.killedCreatureStableID);
        }*/
           // Debug.Log($"shit man... I was just killed. {killedEvent.killedByRuntimeID} did it, I am {killedEvent.killedCreatureStableID}");
    }


 

}
