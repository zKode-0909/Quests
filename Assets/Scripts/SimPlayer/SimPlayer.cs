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
        Debug.Log($"I have taken {damage} damage from {damagerRuntimeID}");
    }

    public void Initialize(int id,int level)
    {
       entityRuntimeID = id;
       entityLevel = level;
    }
}
