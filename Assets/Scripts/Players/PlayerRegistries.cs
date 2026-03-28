using UnityEngine;

public class PlayerRegistration : EntityRegistration
{
    public override void Register(string id)
    {
        EventBus<RegisterInventoryEvent>.Raise(new RegisterInventoryEvent(id));
        EventBus<RegisterQuestLogEvent>.Raise(new RegisterQuestLogEvent(id));
    }
}
