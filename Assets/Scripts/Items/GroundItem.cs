using UnityEngine;

public class GroundItem : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemSettings settings;
    //IRuntimeItem item;

    void IInteractable.HandleInteract(IInteractor interactor)
    {
        EventBus<GatherItemEvent>.Raise(new GatherItemEvent(interactor.EntityRuntimeID, settings.StableID));
        Destroy(gameObject);
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (settings == null) { 
            Debug.LogError($"{name}: GroundItem missing ItemSettings", this);
        }
        
    }

}
