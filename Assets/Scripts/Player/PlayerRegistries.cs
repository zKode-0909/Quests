using UnityEngine;

public class PlayerRegistries
{
    public void Register(int id) {
        EventBus<RegisterInventoryEvent>.Raise(new RegisterInventoryEvent(id));
        EventBus<RegisterQuestLogEvent>.Raise(new RegisterQuestLogEvent(id));
    }
}
