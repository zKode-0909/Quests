using System;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour,IInteractor,IEntity,IDamageable,IPlayer
{
    
    public PlayerMotor motor;
    public StateMachine stateMachine;
    public PlayerQuests playerQuests;
    public PlayerLevelling playerLevelling;
    [SerializeField] EntityRegistration registries;
    public PlayerInventory playerInventory;
    //public PlayerEquipment equipment;
   // public PlayerStats statSnapshot;

    [SerializeField] PlayerBootStrapper bootstrapper;
    [SerializeField] Animator animator;
    [SerializeField] WeaponSettings weapon;
    [SerializeField] Transform holdingHand;
    public CountdownTimer attackCooldownTimer;
    Weapon runtimeWeapon;
    WeaponFactory weaponFactory; 
    public int questLogSize = 25;
    
    List<Timer> timers;
    

    CountdownTimer scanCooldownTimer;
    [SerializeField] float attackCooldown = 0.5f;
    public string ID = "testID";

    public GameObject GameObject => this.gameObject;

    public int EntityRuntimeID => entityRuntimeID;
    public int EntityLevel => playerLevelling.playerLevel;

    public int Level => playerLevelling.playerLevel;

    public int entityRuntimeID;

    // public QuestLog questLog;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, weapon.range);
    }

    public void TakeDamage(float damage,int damagerID)
    {
        Debug.Log("Took Damage");
    }

    public void RequestAttack()
    {
        if (runtimeWeapon != null)
        {
            //var ctx = new AttackContext(this.gameObject);
            attackCooldownTimer.Start();
            //Debug.Log($"attack result: {runtimeWeapon.TryAttack(this.gameObject,this.entityRuntimeID)}");
        }
        else {
            //Debug.Log("No weapon");
        }
        
    }

    private void Awake()
    {
        
        playerInventory = new PlayerInventory();
        registries.Register(entityRuntimeID);
        attackCooldownTimer = new CountdownTimer(weapon.cooldown);
        weaponFactory = new WeaponFactory();
        runtimeWeapon = weaponFactory.CreateWeapon(weapon,holdingHand);
        scanCooldownTimer = new CountdownTimer(10f);

        scanCooldownTimer.OnTimerStart = HandleTimerStart;
        scanCooldownTimer.OnTimerStop = HandleTimerFinish;
        


    }


    void HandleTimerStart() {
    }

    void HandleTimerFinish() {
    }


    public void Intialize(PlayerMotor motor,int runtimeID) {
        this.motor = motor;
        stateMachine = new StateMachine();
        playerLevelling = new PlayerLevelling();
    }
    


    private void Start()
    {
        Debug.Log(bootstrapper.playerRegistry);
        
        if (bootstrapper.playerRegistry.TryGet(entityRuntimeID, out var ent)) {
        }
        
        

        
        timers = new List<Timer>(1) { scanCooldownTimer,attackCooldownTimer };
       // playerQuests = new PlayerQuests();
        //playerQuests.SetPlayerQuestLog(QuestCapacity);

        var locomotionState = new LocomotionState(this,animator);
        var attackState = new AttackState(this,animator);

        At(locomotionState, attackState, new FuncPredicate(() =>  attackCooldownTimer.IsRunning));
        At(attackState, locomotionState, new FuncPredicate(() => !attackCooldownTimer.IsRunning));

        stateMachine.SetState(locomotionState);

        playerQuests.ForceRescanNearby();
    }

    void UpdateAnimator()
    {
       // Debug.Log(Mathf.Lerp(0,600f,currentSpeed));
        animator.SetFloat("Speed", Mathf.InverseLerp(0,7f,motor.GetVelocityMagnitude()));
    }

    void HandleTimers()
    {
        foreach (var timer in timers)
        {
            timer.Tick(Time.deltaTime);
        }
    }


    void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

    void At(IState from, IState to, IPredicate condition)
    {
        // Debug.Log($"condition in playercontrollet: {condition}");
        stateMachine.AddTransition(from, to, condition);

    }

    private void Update()
    {
        HandleTimers();
        if (scanCooldownTimer.IsFinished) { 
            scanCooldownTimer.Start();
        }
        stateMachine.Update();
       
        UpdateAnimator();

    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("I have been hit");
    }
}
