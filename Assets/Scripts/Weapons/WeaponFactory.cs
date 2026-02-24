using UnityEngine;

public class WeaponFactory
{

    public Weapon CreateWeapon(WeaponSettings weaponToSpawn,Transform handSocket)
    {
        var weapon = new Weapon(weaponToSpawn.strategy, weaponToSpawn.weaponName,
            weaponToSpawn.weaponDamage);

        var go = Object.Instantiate(weaponToSpawn.prefab);
        go.transform.SetParent(handSocket, false);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
        return weapon;
    }
}
