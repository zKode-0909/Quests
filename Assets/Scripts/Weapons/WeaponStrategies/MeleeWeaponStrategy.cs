using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponStrategy", menuName = "WeaponStrategies/MeleeWeaponStrategy")]
public class MeleeWeaponStrategy : BaseWeaponStrategy
{
    public override void CollectHits(in AttackContext attackCtx, List<HitContext> results)
    {
        //Debug.Log($"Collecting hits from {attackCtx} using weapon: {weapon}");
        Vector3 attackPos = attackCtx.attacker.transform.position + attackCtx.attacker.transform.forward;
        Collider[] hitEnemies = Physics.OverlapSphere(attackPos,attackCtx.attackRange,damageableLayer);

        
        foreach (Collider collider in hitEnemies) {
            var damaged = collider.GetComponentInParent<IDamageable>();
            if (damaged != null && (collider.gameObject != attackCtx.attacker))
            {
                results.Add(new HitContext(damaged));

            }
       
            
        }
        /*
        foreach (var enemy in hitEnemies)
        {
            Debug.Log(enemy.name);
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }*/
    }

    
}
