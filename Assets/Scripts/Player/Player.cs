using System;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour,IInteractor,IEntity,IDamageable,IPlayer
{
    
    public PlayerMotor motor;
    public PlayerQuests playerQuests;
    public PlayerLevelling playerLevelling;
    public PlayerInventory playerInventory;
    PlayerState playerState;
    Animator animator;
    
    Transform holdingHand;
    
    Weapon runtimeWeapon;
    
    
    public string ID = "testID";

    public GameObject GameObject => this.gameObject;

    public int EntityRuntimeID => entityRuntimeID;
    public int EntityLevel => playerLevelling.playerLevel;

    public int Level => playerLevelling.playerLevel;

    public int entityRuntimeID;

    public void TakeDamage(float damage,int damagerID)
    {
        Debug.Log("Took Damage");
    }

    public void RequestAttack()
    {
        if (runtimeWeapon != null)
        {
            playerState.attackCooldownTimer.Start();
        }
        else {
        }
        
    }

    public void Initialize(PlayerMotor motor,int runtimeID,Weapon weapon,Animator animator,Transform holdingHand,
        PlayerState playerState,PlayerLevelling playerLevelling,PlayerInventory playerInventory,PlayerQuests playerQuests) {
        this.motor = motor;
        this.runtimeWeapon = weapon;
        this.animator = animator;
        this.holdingHand = holdingHand;
        this.entityRuntimeID = runtimeID;
        
        this.playerState = playerState;
        this.playerLevelling = playerLevelling;
        this.playerInventory = playerInventory;
        this.playerQuests = playerQuests;

        
    }
    


    private void Start()
    {
        playerQuests.ForceRescanNearby();
    }

    void UpdateAnimator()
    {
        animator.SetFloat("Speed", Mathf.InverseLerp(0,7f,motor.GetVelocityMagnitude()));
    }

    void HandleTimers()
    {
        foreach (var timer in playerState.timers)
        {
            timer.Tick(Time.deltaTime);
        }
    }


    

    private void Update()
    {
        HandleTimers();
        if (playerState.scanCooldownTimer.IsFinished) { 
            playerState.scanCooldownTimer.Start();
        }
        playerState.stateMachine.Update();
       
        UpdateAnimator();

    }

    private void FixedUpdate()
    {
        playerState.stateMachine.FixedUpdate();
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("I have been hit");
    }

    public Weapon GetPlayerRuntimeWeapon() { 
        return runtimeWeapon;
    }

    public Animator GetPlayerAnimator() { 
        return animator;
    }
}
