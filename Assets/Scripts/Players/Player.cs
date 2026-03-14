using System.Security.Principal;
using UnityEngine;


public abstract class Player : MonoBehaviour, IEntity, IDamageable, IInteractor, IInteractable
{
    public GameObject GameObject => gameObject;

    public int EntityRuntimeID => entityRuntimeID;

    public EntityHealth Health => health;

    public int EntityLevel => level;

    protected Weapon runtimeWeapon;

    protected PlayerState playerState;
    protected Animator animator;

    [SerializeField] protected Transform holdingHand;

    protected EntityHealth health;
    public PlayerMotor playerMotor;

    protected int level;

    protected int entityRuntimeID;

    protected PlayerRegistration registration;
    protected PlayerRegistry registry;
    

    public void Initialize(Animator animator,EntityHealth health,int runtimeID,PlayerRegistration registration,PlayerState playerState,PlayerMotor motor,PlayerRegistry registry)
    {
        this.health = health;
        this.entityRuntimeID = runtimeID;
        this.animator = animator;
        this.registration = registration;
        this.playerState = playerState;
        this.playerMotor = motor;
        this.registry = registry;


        registry.TryRegisterPlayer(this,entityRuntimeID);
        registration.Register(entityRuntimeID);
    }

    public virtual void HandleInteract(IInteractor interactor)
    {
        Debug.Log($"{interactor} has interacted with me.");
    }

    public void TakeDamage(int damage, int damagerRuntimeID)
    {
        throw new System.NotImplementedException();
    }

    public void RequestAttack()
    {
        /*
        if (runtimeWeapon != null)
        {
            playerState.attackCooldownTimer.Start();
        }
        else
        {
        }*/

    }

    void HandleTimers()
    {
        foreach (var timer in playerState.timers)
        {
            timer.Tick(Time.deltaTime);
        }
    }
    /*
    void UpdateAnimator()
    {
        animator.SetFloat("Speed", Mathf.InverseLerp(0, 7f, motor.GetVelocityMagnitude()));
    }*/




    private void Update()
    {
        HandleTimers();
        if (playerState.scanCooldownTimer.IsFinished)
        {
            playerState.scanCooldownTimer.Start();
        }
        playerState.stateMachine.Update();

       // UpdateAnimator();

    }

    private void FixedUpdate()
    {
        playerState.stateMachine.FixedUpdate();
    }



    public Weapon GetPlayerRuntimeWeapon()
    {
        return runtimeWeapon;
    }

    public Animator GetPlayerAnimator()
    {
        return animator;
    }
}
