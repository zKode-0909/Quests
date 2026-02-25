using UnityEngine;

public readonly struct HitContext
{
    public readonly IDamageable target;

    public HitContext(IDamageable damageable) {
        target = damageable;
    }
}
