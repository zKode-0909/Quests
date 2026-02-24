using UnityEngine;

public interface IWeapon
{
    public bool TryAttack(AttackContext attCtx, out HitContext hitCtx);
}
