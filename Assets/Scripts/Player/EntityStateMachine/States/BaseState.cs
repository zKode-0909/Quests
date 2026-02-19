using UnityEngine;

public abstract class BaseState : IState
{
    protected readonly Player player;
    //protected readonly Animator animator;

    protected static readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    protected static readonly int JumpHash = Animator.StringToHash("Jump");
    protected static readonly int DashHash = Animator.StringToHash("Dash");
    protected static readonly int AttackHash = Animator.StringToHash("Attack");
    protected const float crossFadeDuration = 0.1f;

    protected BaseState(Player player)
    {
        this.player = player;
       // this.animator = animator;
    }

    public virtual void FixedUpdate()
    {
        //throw new System.NotImplementedException();
    }

    public virtual void OnExit()
    {
        Debug.Log("exited state");
    }

    public virtual void Update()
    {
        //throw new System.NotImplementedException();
    }

    public virtual void OnEnter()
    {
        //throw new System.NotImplementedException();
    }
}