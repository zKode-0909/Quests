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
        this.player = player;
        this.animator = animator;
    }
}
