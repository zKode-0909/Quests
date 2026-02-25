using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseWeaponStrategy", menuName = "WeaponStrategies/BaseWeaponStrategy")]
public abstract class BaseWeaponStrategy : ScriptableObject
{
    
    [SerializeField] protected LayerMask damageableLayer;
    public abstract void CollectHits(
        in AttackContext attackCtx,
        List<HitContext> results);


}
