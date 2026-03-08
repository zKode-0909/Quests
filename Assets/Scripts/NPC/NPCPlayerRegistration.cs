using UnityEngine;

public class NPCPlayerRegistration : EntityRegistration
{
    public override void Register(int id)
    {
        EventBus<RegisterInventoryEvent>.Raise(new RegisterInventoryEvent(id));
        EventBus<RegisterQuestLogEvent>.Raise(new RegisterQuestLogEvent(id));
    }
}
