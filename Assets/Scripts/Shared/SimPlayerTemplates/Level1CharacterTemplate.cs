using UnityEngine;

[CreateAssetMenu(fileName = "Level1CharacterTemplate", menuName = "SimPlayerCreationTemplate/Level1CharacterTemplate")]
public class Level1CharacterTemplate : ScriptableObject
{
    [SerializeField] public int maxHealth;
    [SerializeField] public int startingLevel;
    [SerializeField] public string StableID;
}
