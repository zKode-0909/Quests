using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    Selector selector;

    private void Start()
    {
        selector = new Selector();
    }

    public bool TryGetSelection(Vector2 mousePos, out ISelectable selected)
    {
        if (selector.TrySelect(mousePos, out var hit))
        {
            selected = hit;
            return true;
        }

        selected = null;
        return false;
    }
}
