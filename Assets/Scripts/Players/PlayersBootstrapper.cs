
using System.Collections.Generic;
using UnityEngine;

public class PlayersBootstrapper : MonoBehaviour
{
    [SerializeField] HumanPlayerBootStrapper humanBootstrapper;
    [SerializeField] SimPlayerBootstrapper simPlayerBootstrapper;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] SimPlayer simPrefab;
    [SerializeField] PlayerTemplateDB playerTemplateDB;

    [SerializeField] PlayerRegistration registration;
    PlayerSpawner spawner;
    PlayerRegistry registry;


    public void Bootstrap() {
        var spawner = new PlayerSpawner(spawnPoints);
        var registry = new PlayerRegistry();
        var registration = new PlayerRegistration();

        humanBootstrapper.BootStrap(registration,spawner,registry);
        simPlayerBootstrapper.Bootstrap(spawner,registry,registration,simPrefab,playerTemplateDB);
    }
}
