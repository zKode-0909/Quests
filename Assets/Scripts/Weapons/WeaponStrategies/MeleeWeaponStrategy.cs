using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponStrategy", menuName = "WeaponStrategies/MeleeWeaponStrategy")]
public class MeleeWeaponStrategy : BaseWeaponStrategy
{
    public override void CollectHits(in AttackContext attackCtx, Weapon weapon, List<HitContext> results)
    {
        Debug.Log($"Collecting hits from {attackCtx} using weapon: {weapon}");
    }
}
