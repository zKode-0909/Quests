using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public CountdownTimer attackCooldownTimer;

    private void Awake()
    {
        attackCooldownTimer = new CountdownTimer(1f);
    }
    public bool TryAttack() {
        attackCooldownTimer.Start();
        Debug.Log("IM FUCKING KILLING THIS NIGGA!");
        return true;
    }

    private void Update()
    {
        attackCooldownTimer.Tick(Time.deltaTime);
    }
}
