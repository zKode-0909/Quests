using UnityEngine;

public readonly struct AttackContext
{
    public readonly GameObject attacker;

    public AttackContext(GameObject att) { 
        attacker = att;
    } 


}
