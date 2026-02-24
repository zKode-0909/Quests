using UnityEngine;

public class CombatController : MonoBehaviour
{
    public CountdownTimer attackCooldownTimer;
    IWeapon weapon;

    private void Awake()
    {
        attackCooldownTimer = new CountdownTimer(1f);
    }
    public bool TryAttack(IWeapon w) {
        weapon = w;
        attackCooldownTimer.Start();
        var aCtx = BuildAttackContext();
        if (w.TryAttack(aCtx,out var hit))
        {
            hit.target.TakeDamage(5f);
        }
        Debug.Log("IM FUCKING KILLING THIS NIGGA!");
        return true;
    }

    AttackContext BuildAttackContext() {
        return new AttackContext(this.gameObject);
    }

    private void Update()
    {
        attackCooldownTimer.Tick(Time.deltaTime);
    }
}
