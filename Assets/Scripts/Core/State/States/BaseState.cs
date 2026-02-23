using UnityEngine;

public abstract class BaseState : IState
{
    //protected readonly Player player;
    //protected readonly Animator animator;
    /*
  */
    protected const float crossFadeDuration = 0.1f;
    
    protected BaseState()
    {
        
       // this.animator = animator;
    }

    public virtual void FixedUpdate()
    {
        //throw new System.NotImplementedException();
    }

    public virtual void OnExit()
    {
    
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