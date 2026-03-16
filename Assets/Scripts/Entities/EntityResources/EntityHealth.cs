using UnityEngine;

public class EntityHealth
{
    int currentHealth;
    int maxHealth;

    public EntityHealth(int maxHealth)
    {
        this.currentHealth = maxHealth;
        this.maxHealth = maxHealth;
    }

    public int ChangeHealth(int delta) {

        if (currentHealth + delta > maxHealth) { 
            delta = maxHealth - currentHealth;
        }

        currentHealth += delta;

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
