using UnityEngine;

public class NPCPlayerRegistration : EntityRegistration
{
    
    public override void Register(string id, bool human)
    {
        //EventBus<RegisterInventoryEvent>.Raise(new RegisterInventoryEvent(id));
        EventBus<RegisterQuestLogEvent>.Raise(new RegisterQuestLogEvent(id,false));
    }
}
