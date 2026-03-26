using UnityEngine;

public readonly struct SelectableData
{
    public readonly int maxHealth;
    public readonly int currentHealth;
    public readonly bool isElite;
    public readonly bool isPlayer;
    public readonly bool isFriendly;
    public readonly bool isHuman;
    public readonly string selectedName;
    public readonly bool inParty;

    public SelectableData(int max,int curr,bool elite,bool player,bool friendly,bool human, string name,bool inParty) {
        this.maxHealth = max;
        this.currentHealth = curr;
        this.isElite = elite;
        this.isPlayer = player;
        this.isFriendly = friendly;
        this.isHuman = human;
        this.selectedName = name;
        this.inParty = inParty;
    }
    
}
