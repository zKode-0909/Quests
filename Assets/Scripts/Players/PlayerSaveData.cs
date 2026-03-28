using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    [SerializeField] private Vector3 position;
    [SerializeField] private string stableID;
    [SerializeField] private string playerName;
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private int level;
    [SerializeField] private PlayerType type;

    public Vector3 Position => position;
    public string StableID => stableID;
    public string PlayerName => playerName;
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public int Level => level;
    public PlayerType Type => type;


    public PlayerSaveData(Vector3 pos,string stable,string name,int currHp,int maxHp,int level,PlayerType type) {
        this.position = pos;
        this.stableID = stable;
        this.playerName = name;
        this.currentHealth = currHp;
        this.maxHealth = maxHp;
        this.level = level;
        this.type = type;   
    }
}
