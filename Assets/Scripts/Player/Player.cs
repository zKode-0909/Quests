using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour,IEntity, IInteractor,IQuester
{
    public PlayerController controller;
    public PlayerMotor motor;
    public StateMachine stateMachine;
    public PlayerQuests playerQuests;
    public PlayerLevelling playerLevelling;
    //public PlayerEquipment equipment;
   // public PlayerStats statSnapshot;
    //public PlayerCombat playerCombatHandler;
    Rigidbody rb;

    
    
    List<Timer> timers;
    CountdownTimer attackCooldownTimer;
    [SerializeField] float attackCooldown = 0.5f;
    public string ID = "testID";

    public GameObject GameObject => this.gameObject;

    public int EntityRuntimeID => entityRuntimeID;

    public QuestLog QuestLog => ((IQuester)playerQuests).QuestLog;

    public HashSet<string> CompletedQuests => ((IQuester)playerQuests).CompletedQuests;

    public int QuesterLevel => ((IQuester)playerQuests).QuesterLevel;

    int entityRuntimeID = 1;

    // public QuestLog questLog;



    public void TakeDamage()
    {
        Debug.Log("Took Damage");
    }

    public void RequestAttack()
    {
        //statSnapshot = new PlayerStats();
        if (!attackCooldownTimer.IsRunning)
        {
            attackCooldownTimer.Start();
        }
    }

    





    private void Awake()
    {
        EntityRegistry.Register(this);
        playerLevelling = new PlayerLevelling();
        
        rb = controller.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        motor = new PlayerMotor(rb, transform, 7f, Camera.main);
        stateMachine = new StateMachine();

        attackCooldownTimer = new CountdownTimer(attackCooldown);
        timers = new List<Timer>(1) { attackCooldownTimer };

       // playerQuests = new PlayerQuests();
        //playerQuests.SetPlayerQuestLog(QuestCapacity);

        var locomotionState = new LocomotionState(this);
        var attackState = new AttackState(this);

        At(locomotionState, attackState, new FuncPredicate(() => attackCooldownTimer.IsRunning));
        At(attackState, locomotionState, new FuncPredicate(() => !attackCooldownTimer.IsRunning));

        stateMachine.SetState(locomotionState);
    }

    private void Start()
    {
        playerQuests.ForceRescanNearby();
    }

    void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

    void At(IState from, IState to, IPredicate condition)
    {
        // Debug.Log($"condition in playercontrollet: {condition}");
        stateMachine.AddTransition(from, to, condition);

    }

    private void Update()
    {
        stateMachine.Update();
        


    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public bool TryAddQuest(Quest quest)
    {
        return ((IQuester)playerQuests).TryAddQuest(quest);
    }
}
