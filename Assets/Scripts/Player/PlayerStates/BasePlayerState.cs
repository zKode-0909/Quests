using UnityEngine;

public class BasePlayerState : BaseState
{
    Player player;
    Animator animator;
    protected static readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    //protected static readonly int JumpHash = Animator.StringToHash("Jump");
    //protected static readonly int DashHash = Animator.StringToHash("Dash");
    protected static readonly int AttackHash = Animator.StringToHash("Attack");
    public BasePlayerState(Player player,Animator animator) : base()
    {
        Debug.Log($"Animator obj = {animator.gameObject.name}");
        Debug.Log($"Controller = {animator.runtimeAnimatorController?.name}");
        Debug.Log($"LayerCount = {animator.layerCount}");
        Debug.Log($"Locomotion hash is: {LocomotionHash}");
        this.player = player;
        this.animator = animator;
    }
}
