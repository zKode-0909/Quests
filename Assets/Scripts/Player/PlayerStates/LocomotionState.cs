using UnityEngine;
using System;

public class LocomotionState : BaseState
{
    static readonly int SpeedHash = Animator.StringToHash("Speed");
    private float speed;
    public LocomotionState(Player player) : base(player)
    {

    }

    public override void OnEnter()
    {

        UnityEngine.Debug.Log($"CurrentSpeed {speed}");



        Debug.Log("entering locomotion");
        //animator.CrossFade(LocomotionHash, crossFadeDuration);
    }

    public override void FixedUpdate()
    {
        // call player's move logic
        //Debug.Log("in loco checkspring");
        //Debug.Log($"Animator Speed param = {animator.GetFloat("Speed")}");
        //speed = Mathf.Lerp(0f, 1f, player.speed);
        //Debug.Log($"Animator Speed param = {animator.GetFloat("Speed")}");
        // animator.SetFloat(SpeedHash, speed);
        player.motor.Move();
    }

    public override void OnExit()
    {
        Debug.Log("leaving locostate");
    }
}
