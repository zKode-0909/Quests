using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseWeaponStrategy", menuName = "WeaponStrategies/BaseWeaponStrategy")]
public abstract class BaseWeaponStrategy : ScriptableObject
{
    public abstract void CollectHits(
        in AttackContext attackCtx,
        Weapon weapon,
        List<HitContext> results);
}
