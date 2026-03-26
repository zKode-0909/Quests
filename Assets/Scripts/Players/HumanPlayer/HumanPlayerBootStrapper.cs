using UnityEngine;
using UnityEngine.Playables;

public class HumanPlayerBootStrapper : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    HumanPlayerController controller;
    [SerializeField] HumanPlayer player;
    [SerializeField] Camera cam;
    [SerializeField] InteractionManager interactionManager;
    [SerializeField] HoverManager hoverManager;
    [SerializeField] OrbitCamera orbitCamera;
    
    PlayerRegistration registration;
    [SerializeField] Animator animator;
    [SerializeField] PlayerInputReader inputReader;
    [SerializeField] LayerMask questGiverLayerMask;


    


    //PlayerMotor motor;
    PlayerSpawner spawner;
    PlayerState playerState;
    PlayerLevelling playerLevelling;
    HumanPlayerInventoryToggle playerInventoryToggle;
    EntityHealth health;
    HumanPlayerMotor motor;
    HumanPlayerQuests playerQuests;
    PlayerRegistry playerRegistry;

    public void BootStrap(PlayerRegistration reg,PlayerSpawner spawn,PlayerRegistry registry) {


        spawner = spawn;
        registration = reg;
        playerRegistry = registry;

        var runtimePlayer = Instantiate(player);
        runtimePlayer.gameObject.SetActive(false);

        var runtimeRb = runtimePlayer.GetComponent<Rigidbody>();
        var runtimeAnimator = runtimePlayer.GetComponent<Animator>();

        controller = runtimePlayer.GetComponent<HumanPlayerController>();

        var health = new EntityHealth(100);

        var playerQuests = new HumanPlayerQuests();
        playerQuests.Initialize(runtimePlayer, questGiverLayerMask);

        var motor = new HumanPlayerMotor();
        motor.InitializeHumanPlayerMotor(runtimeRb, runtimePlayer.transform, cam, 7f);

        var playerState = new PlayerState(runtimePlayer, runtimeAnimator);
        var playerLevelling = new PlayerLevelling();
        var playerInventoryToggle = new HumanPlayerInventoryToggle();

        var playerRuntimeId = RuntimeIDGenerator.GetNext();
          
        controller.Initialize(runtimeRb, inputReader, runtimePlayer);

        runtimePlayer.Initialize(
            runtimeAnimator,
            health,
            playerRuntimeId,
            registration,
            playerState,
            motor,
            registry,
            "Human Player"
        );

        runtimePlayer.InitializeHumanPlayer(playerQuests, playerInventoryToggle);

        spawner.SpawnPlayer(runtimePlayer);

        hoverManager.SetTarget(runtimePlayer.transform);
        interactionManager.SetTarget(runtimePlayer);
        orbitCamera.SetFocus(runtimePlayer.transform);





    }
}
