using UnityEngine;

public class SimulationManager
{
    PlayerTemplateDB playerTemplateDB;

    public void Initialize(PlayerTemplateDB db) { 
        playerTemplateDB = db;

        RequestSimPlayerGeneration();
    }

    public void RequestSimPlayerGeneration()
    {
        if (playerTemplateDB.TryGetTemplateDef("HumanTemplate", out var characterTemplate))
        {
            Debug.Log($"about to request sim player generatoin");
            EventBus<RequestGenerateSimPlayerEvent>.Raise(
                new RequestGenerateSimPlayerEvent(characterTemplate.StableID));
        }
    }
}
