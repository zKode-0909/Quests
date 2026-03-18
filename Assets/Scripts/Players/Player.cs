using Codice.CM.Common;
using System;
using System.Security.Principal;
using UnityEngine;


public abstract class Player : MonoBehaviour, IEntity, IDamageable, IInteractor, IInteractable,ISelectable
{
    public GameObject GameObject => gameObject;

    public int EntityRuntimeID => entityRuntimeID;

    public EntityHealth Health => health;

    public int EntityLevel => level;

    protected Weapon runtimeWeapon;

    protected PlayerState playerState;
    protected Animator animator;

    public event Action<int> healthChangedEvent;

    [SerializeField] protected Transform holdingHand;

    protected EntityHealth health;
    public PlayerMotor playerMotor;

    protected int level;

    protected int entityRuntimeID;

    protected PlayerRegistration registration;
    protected PlayerRegistry registry;

    protected string playerName;


    public CountdownTimer damageTimer;

    bool hitZero = false;

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

        damageTimer = new CountdownTimer(1);

        damageTimer.OnTimerStart += TestDamage;
        damageTimer.OnTimerStop += ResetDamageTimer;

    }

    public virtual void HandleInteract(IInteractor interactor)
    {
        Debug.Log($"{interactor} has interacted with me.");
    }

    public void TakeDamage(int damage, int damagerRuntimeID)
    {
        //Debug.Log($"I have taken {damage} damage, I now have {health.GetCurrentHealth()} out of {health.GetMaxHealth()} health");
        health.ChangeHealth(damage);
        healthChangedEvent?.Invoke(health.GetCurrentHealth());
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




    protected virtual void Update()
    {
        HandleTimers();
        if (playerState.scanCooldownTimer.IsFinished)
        {
            playerState.scanCooldownTimer.Start();
        }
        playerState.stateMachine.Update();
        damageTimer.Tick(Time.deltaTime);

        // UpdateAnimator();

    }

    protected virtual void FixedUpdate()
    {
        playerState.stateMachine.FixedUpdate();
    }



    public Weapon GetPlayerRuntimeWeapon()
    {
        return runtimeWeapon;
    }

    public virtual SelectableData SendSelectionData()
    {
        return new SelectableData(health.GetMaxHealth(),health.GetCurrentHealth(),false,true,true,false,$"testyname {UnityEngine.Random.Range(0,1000)}");
    }

    public Animator GetPlayerAnimator()
    {
        return animator;
    }

    private void ResetDamageTimer()
    {
        damageTimer.Reset();
        damageTimer.Start();
    }

    private void TestDamage()
    {
        if (health.GetCurrentHealth() == 0)
        {
            hitZero = true;
        }

        if (hitZero == false)
        {
            Debug.Log("TAKING DAMAGE");
            TakeDamage(UnityEngine.Random.Range(-10,-2), 55);
        }
        else
        {
            TakeDamage(UnityEngine.Random.Range(1,10), 55);
            if (health.GetCurrentHealth() == health.GetMaxHealth()) {
                hitZero = false;
            }
        }

    }

    public virtual void HandlePartyInvite(int partyID) {
        Debug.Log("base party invite");
    }

    protected virtual void Start() {
        damageTimer.Start();
    }
}
