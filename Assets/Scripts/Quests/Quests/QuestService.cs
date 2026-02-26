using UnityEngine;

public class QuestService
{
    QuestLogController questLogController;
    private QuestLogRegistry logRegistry;
    QuestFactory questFactory;
    EventBinding<KilledEvent> killedEventBinding;

    public void Initialize(QuestFactory factory, QuestLogRegistry registry,QuestLogController logController)
    {
        logRegistry = registry;
        questFactory = factory;
        questLogController = logController;    

        killedEventBinding = new EventBinding<KilledEvent>(HandleKilledEvent);

        EventBus<KilledEvent>.Register(killedEventBinding);
    }

    public void Dispose() {
        EventBus<KilledEvent>.Deregister(killedEventBinding);
    }


    void HandleKilledEvent(KilledEvent evt) {
        Debug.Log($"the quest log that will need to be updated is...");
        if (logRegistry.TryGet(evt.killedByRuntimeID, out var log))
        {
            questLogController.RequestIncrementQuestObjective(log,evt.killedCreatureStableID);
        }
           // Debug.Log($"shit man... I was just killed. {killedEvent.killedByRuntimeID} did it, I am {killedEvent.killedCreatureStableID}");
    }


 

}
