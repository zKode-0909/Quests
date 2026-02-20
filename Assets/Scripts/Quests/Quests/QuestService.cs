using UnityEngine;

public class QuestService
{
    private QuestLogRegistry logRegistry;
    QuestFactory questFactory;
    EventBinding<RequestAcceptQuest> acceptReqBinding;

    public void Initialize(QuestFactory factory, QuestLogRegistry registry)
    {
        logRegistry = registry;
        questFactory = factory;
        acceptReqBinding = new EventBinding<RequestAcceptQuest>(OnAcceptRequested);

        EventBus<RequestAcceptQuest>.Register(acceptReqBinding);
    }

    public void Dispose() { 
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

}
