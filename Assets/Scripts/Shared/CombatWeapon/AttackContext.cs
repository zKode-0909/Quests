using UnityEngine;

public readonly struct AttackContext
{
    public readonly GameObject attacker;
    public readonly float attackRange;


    public AttackContext(GameObject att,float dmg,float range) { 
        attacker = att;
        attackRange = range;
    } 


}
