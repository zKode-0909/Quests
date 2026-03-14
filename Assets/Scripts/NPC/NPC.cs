using UnityEngine;

public class NPC : MonoBehaviour,IDamageable,IEntity,ICharacter
{

    EntityHealth health;
    [SerializeField] NPCSettings settings;
    [SerializeField] NPCBootStrapper bootstrapper;
    [SerializeField] EntityRegistration registration;

    public GameObject GameObject => this.gameObject;

    public int EntityRuntimeID => runTimeID;

    public int EntityLevel => currentLevel;

    public string StableID => settings.StableID;



    EntityHealth IEntity.Health => throw new System.NotImplementedException();



    int runTimeID;
    int currentLevel;




    public void TakeDamage(int damage,int changerRuntimeID)
    {
        health.ChangeHealth(damage);
        Debug.Log($"GOT DAMN NIGGA! I JUST GOT HIT for {damage} damage, my shit be bleedin! I now have {health.GetHealth()}");
        if (health.GetHealth() <= 0) {
            Debug.Log("I be dead");
            Die(changerRuntimeID);
        }
        
    }

    void Die(int killerID) {
        EventBus<KilledEvent>.Raise(new KilledEvent(killerID,StableID));
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
        runTimeID = RuntimeIDGenerator.GetNext();
        currentLevel  = settings.startingLevel;
        health = new EntityHealth(settings.startingHealth);
        registration.Register(runTimeID);
        //bootstrapper.characterRegistry.Register(this);
        if (bootstrapper.characterRegistry.TryGet(StableID, out var ent))
        {
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void Say(string words) {
        Debug.Log($"{words}");
    }
}
