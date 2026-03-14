using UnityEngine;

public class SimPlayerBootstrapper : MonoBehaviour 
{
    SimPlayerGenerator simPlayerGenerator;
    SimPlayerFactory simPlayerFactory;
    PlayerSpawner playerSpawner;
    PlayerRegistry playerRegistry;
    

    SimPlayer simPlayerPrefab;
    PlayerTemplateDB playerTemplateDB;

    public void Bootstrap(PlayerSpawner spawner,PlayerRegistry registry,PlayerRegistration registration,SimPlayer simPrefab,PlayerTemplateDB templateDB) {
        simPlayerPrefab = simPrefab;
        playerTemplateDB = templateDB;
        playerSpawner = spawner;
        playerRegistry = registry;
        simPlayerFactory = new SimPlayerFactory(simPlayerPrefab,registration,registry);
        simPlayerGenerator = new SimPlayerGenerator();
        simPlayerGenerator.Initialize(simPlayerFactory, playerRegistry, playerSpawner,playerTemplateDB);

    }
}
