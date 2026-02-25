using UnityEngine;

public class QuestService
{
    
    private QuestLogRegistry logRegistry;
    QuestFactory questFactory;
    EventBinding<KilledEvent> killedEventBinding;

    public void Initialize(QuestFactory factory, QuestLogRegistry registry)
    {
        logRegistry = registry;
        questFactory = factory;

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
            Debug.Log(log.GetQuests());
        }
           // Debug.Log($"shit man... I was just killed. {killedEvent.killedByRuntimeID} did it, I am {killedEvent.killedCreatureStableID}");
    }


 

}
