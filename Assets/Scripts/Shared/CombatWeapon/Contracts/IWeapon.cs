using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public List<HitContext> ExecuteAttack(AttackContext attCtx);
}
