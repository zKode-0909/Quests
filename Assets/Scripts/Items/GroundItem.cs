using System;
using UnityEngine;

public class GroundItem : MonoBehaviour, IInteractable,ISelectable
{
    [SerializeField] private ItemSettings settings;

    public int EntityRuntimeID => RuntimeIDGenerator.GetNext();

    public string StableID => "TestRock";

    public SelectableType SelectableType => throw new NotImplementedException();

    public event Action<int> healthChangedEvent;

    //IRuntimeItem item;

    void IInteractable.HandleInteract(IInteractor interactor)
    {
        EventBus<GatherItemEvent>.Raise(new GatherItemEvent(interactor.StableID, settings.StableID));
        Destroy(gameObject);
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (settings == null) { 
            Debug.LogError($"{name}: GroundItem missing ItemSettings", this);
        }
        
    }

    public SelectableData SendSelectionData()
    {
        return new SelectableData(0, 0, false, false, false, false, "Rock", false);
    }

    public void UpdatePartyStatus(bool status)
    {
        throw new NotImplementedException();
    }
}
