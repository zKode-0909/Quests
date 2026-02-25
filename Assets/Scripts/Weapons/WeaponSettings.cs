using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSettings", menuName = "Scriptable Objects/WeaponSettings")]
public class WeaponSettings : ScriptableObject
{
    public BaseWeaponStrategy strategy;
    public string weaponName;
    public int weaponDamage;
    public GameObject prefab;
    public float cooldown;
    public float range;
    
}
