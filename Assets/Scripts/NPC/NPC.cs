using UnityEngine;

public class NPC : MonoBehaviour,IDamageable,IEntity,ICharacter
{

    EntityHealth health;
    [SerializeField] NPCSettings settings;
    [SerializeField] NPCBootStrapper bootstrapper; 

    public GameObject GameObject => this.gameObject;

    public int EntityRuntimeID => runTimeID;

    public int EntityLevel => currentLevel;

    int runTimeID;
    int currentLevel;



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

        runTimeID = this.GetEntityId();
        currentLevel  = settings.startingLevel;
        health = new EntityHealth(settings.startingHealth);
        bootstrapper.characterRegistry.Register(this);
        if (bootstrapper.characterRegistry.TryGet(runTimeID, out var ent))
        {
            Debug.Log($"{ent} has been succesfully registered");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
