using UnityEngine;

public readonly struct PlayerDTO
{
    public readonly int playerRuntimeId;
    public readonly int playerLevel;
    public readonly string playerName;
    public readonly int playerMaxHealth;
    public readonly int playerCurrentHealth;
    public readonly ISelectable selectable;

    public PlayerDTO(int id, int level, string name, int maxHealth, int currentHealth,ISelectable selected) { 
        this.playerRuntimeId = id;
        this.playerLevel = level;
        this.playerName = name;
        this.playerMaxHealth = maxHealth;
        this.playerCurrentHealth = currentHealth;
        this.selectable = selected;
    }
}
