using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public bool TryAttack(GameObject attacker,string attackerID);
}
