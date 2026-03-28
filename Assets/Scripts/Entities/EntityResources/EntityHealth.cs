using UnityEngine;

public class EntityHealth
{
    int currentHealth;
    int maxHealth;

    public EntityHealth(int currentHealth,int maxHealth)
    {
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
    }

    public int ChangeHealth(int delta)
    {
        currentHealth = Mathf.Clamp(currentHealth + delta, 0, maxHealth);
        return currentHealth;
    }

    public int ChangeMaxHealth(int newMaxHealth) { 
        this.maxHealth = newMaxHealth;

        return maxHealth;
    }

    public int GetMaxHealth() { 
        return maxHealth;
    }

    public int GetCurrentHealth() { 
        return currentHealth;
    }
}
