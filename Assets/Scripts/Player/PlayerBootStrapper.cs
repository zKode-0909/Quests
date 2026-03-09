using UnityEngine;

public class PlayerBootStrapper : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] PlayerController controller;
    [SerializeField] Player player;
    [SerializeField] PlayerQuests playerQuests;
    public PlayerRegistry playerRegistry;

    PlayerMotor motor;

    public void BootStrap() {

        motor = new PlayerMotor(rb, this.gameObject.transform, 7f, Camera.main);

        var playerRuntimeId = RuntimeIDGenerator.GetNext();

        player.Intialize(motor,playerRuntimeId);
        playerRegistry = new PlayerRegistry();
        playerRegistry.Register(player);
    }
}
