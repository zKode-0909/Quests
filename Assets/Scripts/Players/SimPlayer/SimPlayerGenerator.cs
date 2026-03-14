using UnityEngine;

public class SimPlayerGenerator
{
    SimPlayerFactory simPlayerFactory;
    PlayerRegistry playerRegistry;
    PlayerSpawner simPlayerSpawner;
    PlayerTemplateDB playerTemplateDB;

    EventBinding<RequestGenerateSimPlayerEvent> generateSimPlayerReqBinding;

    public void Initialize(SimPlayerFactory factory,PlayerRegistry registry,PlayerSpawner spawner,PlayerTemplateDB db) { 
        this.simPlayerFactory = factory;
        this.playerRegistry = registry;
        this.simPlayerSpawner = spawner;
        this.playerTemplateDB = db;

        generateSimPlayerReqBinding = new EventBinding<RequestGenerateSimPlayerEvent>(TryGenerateSimPlayer);
        EventBus<RequestGenerateSimPlayerEvent>.Register(generateSimPlayerReqBinding);
    }

    public void Dispose() {
        EventBus<RequestGenerateSimPlayerEvent>.Deregister(generateSimPlayerReqBinding);
    }

    public void TryGenerateSimPlayer(RequestGenerateSimPlayerEvent evt) {
        if (playerTemplateDB.TryGetTemplateDef(evt.templateToGenerate,out var template)) {
            var generatedPlayer = simPlayerFactory.CreateSimPlayer(template);
            if (playerRegistry.TryGetPlayer(generatedPlayer.EntityRuntimeID,out var player))
            {
                simPlayerSpawner.SpawnPlayer(player);
            }
        }
        
    }
}
