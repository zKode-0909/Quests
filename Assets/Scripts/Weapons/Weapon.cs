using System.Collections.Generic;
using UnityEngine;

public class Weapon : IWeapon
{
    BaseWeaponStrategy strategy;
    string weaponName;
    int damage;
    GameObject weapon;
    List<HitContext> hits = new List<HitContext>();

    public Weapon(BaseWeaponStrategy strat,string name,int dmg) { 
        strategy = strat;
        weaponName = name;
        damage = dmg;   
      

    }

    
    public List<HitContext> ExecuteAttack(AttackContext attCtx)
    {
        hits.Clear();
        Debug.Log($"about to execute weapon strat for: {attCtx.attacker.name}");
        strategy.CollectHits(attCtx, this, hits);

        Debug.Log($"just hit {hits} damagables");

        return hits;
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
