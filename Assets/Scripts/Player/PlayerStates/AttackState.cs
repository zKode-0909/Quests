using UnityEngine;
using System;

public class AttackState : BasePlayerState
{
    Animator animator;
    Player player;
    public AttackState(Player player,Animator animator) : base(player,animator)
    {
        this.animator = animator;
        this.player = player;
    }

    public override void OnEnter()
    {
        Debug.Log("ENTERING ATTACK");
        // player.playerCombatHandler.TryAttack(player.statSnapshot,player.equipment.weapon);
        Debug.Log($"crossfade is {crossFadeDuration}");
        animator.CrossFade(AttackHash, crossFadeDuration);
    }

    public override void FixedUpdate()
    {
       
        player.motor.Move();
    }

    public override void OnExit()
    {
        Debug.Log("leaving attackstate");
       
    }
}

