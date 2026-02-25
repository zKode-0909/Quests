using UnityEngine;

public class EntityHealth
{
    float currentHealth;
    int maxHealth;

    public EntityHealth(int maxHealth)
    {
        this.currentHealth = maxHealth;
        this.maxHealth = maxHealth;
    }

    public float ChangeHealth(float delta) {

        if (currentHealth + delta > maxHealth) { 
            delta = maxHealth - currentHealth;
        }

        currentHealth += delta;

        return currentHealth;   

        
        
    }

    public float GetHealth() { 
        return currentHealth;
    }
}
