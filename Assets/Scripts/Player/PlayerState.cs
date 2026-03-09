using System.Collections.Generic;
using UnityEngine;
using static Codice.Client.Common.Connection.AskCredentialsToUser;

public class PlayerState
{
    public StateMachine stateMachine;
    public List<Timer> timers;

    public CountdownTimer attackCooldownTimer;
    public CountdownTimer scanCooldownTimer;


    public PlayerState(Player player,Animator animator,Weapon runtimeWeapon) {
        stateMachine = new StateMachine();


        

        scanCooldownTimer = new CountdownTimer(10f);
        attackCooldownTimer = new CountdownTimer(runtimeWeapon.GetCooldown());

        timers = new List<Timer>(2) { scanCooldownTimer, attackCooldownTimer };


        var locomotionState = new LocomotionState(player, animator);
        var attackState = new AttackState(player, animator);

        At(locomotionState, attackState, new FuncPredicate(() => attackCooldownTimer.IsRunning));
        At(attackState, locomotionState, new FuncPredicate(() => !attackCooldownTimer.IsRunning));

        stateMachine.SetState(locomotionState);
    }



    void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

    void At(IState from, IState to, IPredicate condition)
    {
        // Debug.Log($"condition in playercontrollet: {condition}");
        stateMachine.AddTransition(from, to, condition);

    }
}
