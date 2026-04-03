using UnityEngine;
using System;
using Unity.VisualScripting.YamlDotNet.Core;
using Codice.Utils;
using Codice.Client.BaseCommands.BranchExplorer;

public class PlayerFactory
{
    PlayerSpawner playerSpawner;
    PlayerRegistration playerRegistration;
    PlayerRegistry playerRegistry;

    public PlayerFactory(PlayerSpawner spawner,PlayerRegistration registration,PlayerRegistry regisry) { 
        this.playerSpawner = spawner;
        this.playerRegistration = registration;
        this.playerRegistry = regisry;
    }

    public Player BuildHumanPlayerFromData(PlayerSaveData data,HumanPlayerView humanPlayerPrefab,
        PlayerInputReader inputReader,HoverManager hoverManager,InteractionManager interactionManager,
        OrbitCamera orbitCamera,LayerMask questGiverLayerMask) {


        var runtimePlayer = UnityEngine.Object.Instantiate(humanPlayerPrefab);
        runtimePlayer.gameObject.SetActive(false);
        
        var runtimeRb = runtimePlayer.GetComponent<Rigidbody>();
        var runtimeAnimator = runtimePlayer.GetComponent<Animator>();

        var controller = new HumanPlayerController();

        var player = new Player();

        var health = new EntityHealth(data.CurrentHealth, data.MaxHealth);

        var playerRuntimeId = RuntimeIDGenerator.GetNext();

       // var playerState = new PlayerState(runtimePlayer, runtimeAnimator);
        var playerLevelling = new PlayerLevelling();
        var playerInventoryToggle = new HumanPlayerInventoryToggle();

        var motor = new HumanPlayerMotor();
        motor.InitializeHumanPlayerMotor(runtimeRb, runtimePlayer.transform, Camera.main, 7f);

        controller.Initialize(runtimeRb, inputReader, player, humanPlayerPrefab,motor);

        player.Initialize(health, playerRuntimeId,data.PlayerName, data.StableID, PlayerType.Human, controller);

        runtimePlayer.Initialize(player,controller);

         

         

         

        // var playerQuests = new HumanPlayerQuests();
        // playerQuests.Initialize(runtimePlayer, questGiverLayerMask);

         

         //controller.Initialize(runtimeRb, inputReader, runtimePlayer);

         //var menuToggle = new HumanMenuToggle();


        // runtimePlayer.InitializeHumanPlayer(playerQuests, playerInventoryToggle, menuToggle);

         if (playerRegistry.TryRegisterPlayer(player, data.StableID))
         {
            playerRegistration.Register(data.StableID,true);
            playerSpawner.SpawnPlayer(runtimePlayer, data.Position);

             hoverManager.SetTarget(runtimePlayer.transform);
             interactionManager.SetTarget(runtimePlayer);
             orbitCamera.SetFocus(runtimePlayer.transform);

             

             return player;

         }
         else {
             return null;
         }
        
      
        
    }

    public SimPlayer BuildSimPlayerFromData(PlayerSaveData data,SimPlayer simPlayerPrefab) {
        var motor = new SimPlayerMotor();
        /*
        var player = UnityEngine.Object.Instantiate(simPlayerPrefab);
        // var runTimeWeapon = weaponFactory.CreateWeapon(template.WeaponSettings);
        var animator = player.GetComponent<Animator>();
        var health = new EntityHealth(data.CurrentHealth, data.MaxHealth);
        var playerState = new PlayerState(player, animator);


        player.gameObject.SetActive(false);
        player.Initialize(animator, health, RuntimeIDGenerator.GetNext(),
            playerState, motor, data.PlayerName, data.StableID, PlayerType.Sim);

        if (playerRegistry.TryRegisterPlayer(player, data.StableID))
        {
            playerSpawner.SpawnPlayer(player, data.Position);

            return player;
        }
        else {
            return null;
        }*/

        return null;
    }

    public Player BuildNewHumanPlayer(HumanPlayerView humanPlayerPrefab, PlayerInputReader inputReader, HoverManager hoverManager, InteractionManager interactionManager,
        OrbitCamera orbitCamera, LayerMask questGiverLayerMask) {

        var runtimePlayer = UnityEngine.Object.Instantiate(humanPlayerPrefab);
        runtimePlayer.gameObject.SetActive(false);

        var runtimeRb = runtimePlayer.GetComponent<Rigidbody>();
        var runtimeAnimator = runtimePlayer.GetComponent<Animator>();

        var controller = new HumanPlayerController();

        var player = new Player();

        var health = new EntityHealth(100, 100);

        var playerRuntimeId = RuntimeIDGenerator.GetNext();

        // var playerState = new PlayerState(runtimePlayer, runtimeAnimator);
        var playerLevelling = new PlayerLevelling();
        var playerInventoryToggle = new HumanPlayerInventoryToggle();

        var motor = new HumanPlayerMotor();
        motor.InitializeHumanPlayerMotor(runtimeRb, runtimePlayer.transform, Camera.main, 7f);

        controller.Initialize(runtimeRb, inputReader, player, humanPlayerPrefab, motor);

        player.Initialize(health, playerRuntimeId, "ButtholeSurfersVidIsOn", "testHumanStableID", PlayerType.Human, controller);

        runtimePlayer.Initialize(player, controller);

        if (playerRegistry.TryRegisterPlayer(player, "TestHumanPlayerStableID"))
        {
            playerRegistration.Register("TestHumanPlayerStableID",true);
            playerSpawner.SpawnPlayer(runtimePlayer);

            hoverManager.SetTarget(runtimePlayer.transform);
            interactionManager.SetTarget(runtimePlayer);
            orbitCamera.SetFocus(runtimePlayer.transform);



            return player;

        }
        else
        {
            return null;
        }

     

    }

    public void BuildNewSimPlayer() { 
    
    }
}
