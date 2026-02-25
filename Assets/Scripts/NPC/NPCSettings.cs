using UnityEngine;

[CreateAssetMenu(fileName = "NPCSettings", menuName = "Scriptable Objects/NPCSettings")]
public class NPCSettings : ScriptableObject
{
    public string NPCName;
    public int startingHealth;
    public string StableID;
}
