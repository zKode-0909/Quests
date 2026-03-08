using UnityEngine;
using System;

public class LocomotionState : BasePlayerState
{
    static readonly int SpeedHash = Animator.StringToHash("Speed");
    private float speed;

    Player player;
    Animator animator;

    public LocomotionState(Player player,Animator animator) : base(player,animator)
    {
        this.player = player;
        this.animator = animator;
    }

    public override void OnEnter()
    {
        /*
        UnityEngine.Debug.Log($"CurrentSpeed {speed}");


        Debug.Log($"Animator obj = {animator.gameObject.name}");
        Debug.Log($"Controller = {animator.runtimeAnimatorController?.name}");
        Debug.Log($"LayerCount = {animator.layerCount}");
        Debug.Log("entering locomotion");
        Debug.Log($"Locomotion hash is: {LocomotionHash}");*/
        animator.CrossFade(LocomotionHash, crossFadeDuration,0);
    }

    public override void FixedUpdate()
    {
        /*
        // call player's move logic
        //Debug.Log("in loco checkspring");
        //Debug.Log($"Animator Speed param = {animator.GetFloat("Speed")}");
        speed = Mathf.Lerp(0f, 1f, player.motor.GetCurrentSpeed());
        //Debug.Log($"Animator Speed param = {animator.GetFloat("Speed")}");
        animator.SetFloat(SpeedHash, speed);
        Debug.Log($"setting speed to {speed}");
        player.motor.Move();*/

        player.motor.Move(); // apply velocity first
        /*
        float v = player.motor.GetVelocityMagnitude();   // or rb.velocity.magnitude
        float max = player.motor.GetBaseSpeed();         // 7f
        float speed01 = Mathf.Clamp01(v / max);

        animator.SetFloat(SpeedHash, speed01 * 1000);
        */
        /*
        Debug.Log(
        $"AnimatorGO={animator.gameObject.name} " +
        $"AnimID={animator.GetInstanceID()} " +
        $"Controller={animator.runtimeAnimatorController?.name}");

        float a = animator.GetFloat("Speed");
        Debug.Log($"AfterSet Speed={a}");

        animator.Update(0f); // forces animator evaluation immediately
        float b = animator.GetFloat("Speed");
        Debug.Log($"AfterAnimatorUpdate Speed={b}");*/

        //var st = animator.GetCurrentAnimatorStateInfo(0);
        //Debug.Log($"Locomotion normalizedTime={st.normalizedTime}");

    }

    public override void OnExit()
    {
        Debug.Log("leaving locostate");
    }
}
