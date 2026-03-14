using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    Selector selector;
    ISelectable currentSelection;
    private void Start()
    {
        selector = new Selector();
    }

    void Update()
    {
        
    }

    public bool TryGetSelection(Vector2 mousePos,out ISelectable selected) {
        if (selector.TrySelect(mousePos, out var s) && s.EntityRuntimeID != currentSelection.EntityRuntimeID) { 
            selected = s;
            return true;
        }

        selected = null;
        return false;
    }
}
