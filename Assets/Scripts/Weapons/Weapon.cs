using System.Collections.Generic;
using UnityEngine;

public class Weapon : IWeapon
{
    BaseWeaponStrategy strategy;
    string weaponName;
    int damage;
    GameObject weapon;
    float attackRange;
    List<HitContext> hits = new List<HitContext>();

    public Weapon(BaseWeaponStrategy strat,string name,int dmg,float range) { 
        strategy = strat;
        weaponName = name;
        damage = dmg; 
        attackRange = range;

    }

    


    public bool TryAttack(GameObject attacker,int runtimeID)
    {
        var attCtx = new AttackContext(attacker,damage,attackRange);
        hits.Clear();
       // Debug.Log($"about to execute weapon strat for: {attCtx.attacker.name}");
        strategy.CollectHits(attCtx, hits);

       // Debug.Log($"just hit {hits.Count} damagables");

        foreach (var hit in hits) {
            //Debug.Log($"didnt hit: {hit.target}");
            hit.target.TakeDamage(-damage,runtimeID);
        }

        return true;

        //return hits;
        /*
        Debug.Log($"attack context during attack: attacker = {attCtx.attacker}");

        if (attCtx.attacker.TryGetComponent<IDamageable>(out var damaged))
        {
            hitCtx = new HitContext(damaged, 6f);
            return true;
        }
        else {
            hitCtx = new HitContext();
            return false;
        }*/

        

        
    }
}
