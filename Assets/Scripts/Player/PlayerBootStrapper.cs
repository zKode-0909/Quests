using UnityEngine;
using UnityEngine.Playables;

public class PlayerBootStrapper : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] PlayerController controller;
    [SerializeField] Player player;
    [SerializeField] PlayerQuests playerQuests;
    [SerializeField] WeaponSettings weapon;
    [SerializeField] Transform handSocket;
    [SerializeField] EntityRegistration registries;
    [SerializeField] Animator animator;
    [SerializeField] PlayerInputReader inputReader;
    

    WeaponFactory weaponFactory;

    PlayerMotor motor;
    PlayerState playerState;
    PlayerLevelling playerLevelling;
    PlayerInventory playerInventory;

    public void BootStrap() {

        weaponFactory = new WeaponFactory();

        var runtimeWeapon = weaponFactory.CreateWeapon(weapon,handSocket);

        motor = new PlayerMotor(rb, player.transform, 7f, Camera.main);
        playerState = new PlayerState(player,animator,runtimeWeapon);
        playerLevelling = new PlayerLevelling();
        playerInventory = new PlayerInventory();

        var playerRuntimeId = RuntimeIDGenerator.GetNext();
        registries.Register(playerRuntimeId);
        player.Initialize(motor,playerRuntimeId,runtimeWeapon,animator,handSocket,playerState,playerLevelling,playerInventory,playerQuests);

        controller.Initialize(rb, inputReader, player);
        
    }
}
