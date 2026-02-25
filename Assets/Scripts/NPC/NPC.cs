using UnityEngine;

public class NPC : MonoBehaviour,IDamageable
{

    EntityHealth health;
    [SerializeField] NPCSettings settings;


    public void TakeDamage(float damage,int changerRuntimeID)
    {
        health.ChangeHealth(damage);
        Debug.Log($"GOT DAMN NIGGA! I JUST GOT HIT for {damage} damage, my shit be bleedin! I now have {health.GetHealth()}");
        if (health.GetHealth() <= 0) {
            Debug.Log("I be dead");
            Die(changerRuntimeID);
        }
        
    }

    void Die(int killerID) {
        EventBus<KilledEvent>.Raise(new KilledEvent(killerID,settings.StableID));
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = new EntityHealth(settings.startingHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
