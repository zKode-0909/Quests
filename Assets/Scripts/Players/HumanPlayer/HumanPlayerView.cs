using UnityEngine;

public class HumanPlayerView : MonoBehaviour 
{
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rb;

    Player player;
    HumanPlayerController controller;

    

    public Animator Animator => animator;
    public Rigidbody Rb => rb;
    public GameObject Go => gameObject;

    public void Initialize(Player player,HumanPlayerController controller)
    {
        this.player = player;
        this.controller = controller;
    }

    private void Update()
    {
        controller.Motor.Move();
    }



}
