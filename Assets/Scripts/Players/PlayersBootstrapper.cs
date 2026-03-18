
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
    PlayerPartyInviteHandler playerPartyInviteHandler;


    public void Bootstrap() {
        spawner = new PlayerSpawner(spawnPoints);
        registry = new PlayerRegistry();
        registration = new PlayerRegistration();
        playerPartyInviteHandler = new PlayerPartyInviteHandler();
        playerPartyInviteHandler.Initialize(registry);

        humanBootstrapper.BootStrap(registration,spawner,registry);
        simPlayerBootstrapper.Bootstrap(spawner,registry,registration,simPrefab,playerTemplateDB);
    }
}
