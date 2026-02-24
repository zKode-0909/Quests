using UnityEngine;

public class CombatController : MonoBehaviour
{
    public CountdownTimer attackCooldownTimer;
    

    private void Awake()
    {
        attackCooldownTimer = new CountdownTimer(1f);
    }
    public bool TryAttack(IWeapon w) {
        
        attackCooldownTimer.Start();
        var aCtx = BuildAttackContext(w);
        var hitCtx = w.ExecuteAttack(aCtx);
        for (int i = 0; i < hitCtx.Count; i++) {
            Debug.Log($"hit: {i+1} - {hitCtx[i].target}");
        }
    
        Debug.Log("IM FUCKING KILLING THIS NIGGA!");
        return true;
    }

    AttackContext BuildAttackContext(IWeapon weapon) {
        return new AttackContext(this.gameObject);
    }

    private void Update()
    {
        attackCooldownTimer.Tick(Time.deltaTime);
    }
}
