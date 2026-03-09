using UnityEngine;

public class SimPlayer : MonoBehaviour,IEntity,IDamageable,IInteractor,IInteractable
{

    public GameObject GameObject => gameObject;

    public int EntityRuntimeID => entityRuntimeID;

    public int EntityLevel => entityLevel;

    int entityRuntimeID;

    int entityLevel;

    public void HandleInteract(IInteractor interactor)
    {
        Debug.Log($"{interactor} has interacted with me.");
    }

    public void TakeDamage(float damage, int damagerRuntimeID)
    {
        Debug.Log($"I have taken {damage} damage {damagerRuntimeID}");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
