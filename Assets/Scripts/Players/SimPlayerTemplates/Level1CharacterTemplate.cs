using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Level1CharacterTemplate", menuName = "SimPlayerCreationTemplate/Level1CharacterTemplate")]
public class Level1CharacterTemplate : ScriptableObject
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int startingLevel;
    [SerializeField] private string stableID;
    [SerializeField] private BaseVisualVariants visualVariants;
    [SerializeField] private WeaponSettings weaponSettings;

    public int MaxHealth => maxHealth;
    public int StartingLevel => startingLevel;
    public string StableID => stableID;
    public BaseVisualVariants VisualVariants => visualVariants;
    public WeaponSettings WeaponSettings => weaponSettings;

}


