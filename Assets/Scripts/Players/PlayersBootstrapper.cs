
using System.Collections.Generic;
using UnityEngine;

public class PlayersBootstrapper : MonoBehaviour
{
    [SerializeField] HumanPlayerBootStrapper humanBootstrapper;
    [SerializeField] SimPlayerBootstrapper simPlayerBootstrapper;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] SimPlayer simPrefab;
    [SerializeField] PlayerTemplateDB playerTemplateDB;
    [SerializeField] SimPlayer simPlayerPrefab;

    [SerializeField] PlayerRegistration registration;
    PlayerSpawner spawner;
    PlayerRegistry registry;
    PlayerPartyInviteHandler playerPartyInviteHandler;
    PlayerFactory playerFactory;

    [SerializeField] HumanPlayer humanPlayerPrefab;

    [SerializeField] HumanPlayerView humanPlayerView;

    [SerializeField] InteractionManager interactionManager;
    [SerializeField] HoverManager hoverManager;
    [SerializeField] OrbitCamera orbitCamera;

    [SerializeField] PlayerInputReader inputReader;

    [SerializeField] LayerMask questGiverLayerMask;


    public void Bootstrap(PlayerRegistry registry,List<PlayerSaveData> playerSaveData,bool newGame) {
        spawner = new PlayerSpawner(spawnPoints);
        this.registry = registry;
        playerPartyInviteHandler = new PlayerPartyInviteHandler();
        playerPartyInviteHandler.Initialize(registry);

        //humanBootstrapper.BootStrap(registration,spawner,registry);
        //simPlayerBootstrapper.Bootstrap(spawner,registry,registration,simPrefab,playerTemplateDB);

        playerFactory = new PlayerFactory(spawner, registration, registry);

        if (!newGame)
        {
            BuildPlayersFromData(playerSaveData);
        }
        else {
            BuildStartupPlayers();
        }
        
    }

    void BuildPlayersFromData(List<PlayerSaveData> saveData)
    {
        foreach (var data in saveData) {
            Player player = data.Type switch
            {
                PlayerType.Human =>  playerFactory.BuildHumanPlayerFromData(data,humanPlayerView,inputReader,hoverManager,interactionManager,orbitCamera,questGiverLayerMask),
                PlayerType.Sim => playerFactory.BuildSimPlayerFromData(data,simPlayerPrefab),
                _ => throw new System.ArgumentOutOfRangeException()

            };
       
        }

    }

    void BuildStartupPlayers() { 
        playerFactory.BuildNewHumanPlayer(humanPlayerView, inputReader, hoverManager, interactionManager, orbitCamera, questGiverLayerMask);
    }

   
}
