using UnityEngine;

public class SimulationManager
{
    SimPlayerTemplateDB SimPlayerTemplateDB;

    public void Initialize(SimPlayerTemplateDB db) { 
        SimPlayerTemplateDB = db;
    }

    public void RequestSimPlayerGeneration()
    {
        if (SimPlayerTemplateDB.TryGetTemplateDef("testTemplate", out var characterTemplate))
        {
            EventBus<RequestGenerateSimPlayerEvent>.Raise(
                new RequestGenerateSimPlayerEvent(characterTemplate));
        }
    }
}
