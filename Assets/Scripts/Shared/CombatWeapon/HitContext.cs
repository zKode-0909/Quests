using UnityEngine;

public readonly struct HitContext
{
    public readonly IDamageable target;
    public readonly float damage;

    public HitContext(IDamageable damageable,float dmg) {
        this.damage = dmg;
        target = damageable;
    }
}
