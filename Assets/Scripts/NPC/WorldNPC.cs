using UnityEngine;

public class WorldNPC : MonoBehaviour,IDamageable,IEntity
{
    EntityHealth health;
    [SerializeField] NPCSettings settings;

    public GameObject GameObject => this.gameObject;

    public int EntityRuntimeID => runtimeID;


    public int EntityLevel => level;

    int runtimeID;
    int level;

    public void TakeDamage(float damage, int changerRuntimeID)
    {
        health.ChangeHealth(damage);
        Debug.Log($"GOT DAMN NIGGA! I JUST GOT HIT for {damage} damage, my shit be bleedin! I now have {health.GetHealth()}");
        if (health.GetHealth() <= 0)
        {
            Debug.Log("I be dead");
            Die(changerRuntimeID);
        }

    }

    void Die(int killerID)
    {
        EventBus<KilledEvent>.Raise(new KilledEvent(killerID, settings.StableID));
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        runtimeID = RuntimeIDGenerator.GetNext();
        level = settings.startingLevel;
        health = new EntityHealth(settings.startingHealth);
    }
}
