using UnityEngine;
using System;

public class AttackState : BaseState
{

    public AttackState(Player player) : base(player)
    {

    }

    public override void OnEnter()
    {
        Debug.Log("entering attack");
       // player.playerCombatHandler.TryAttack(player.statSnapshot,player.equipment.weapon);
        //animator.CrossFade(LocomotionHash, crossFadeDuration);
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

