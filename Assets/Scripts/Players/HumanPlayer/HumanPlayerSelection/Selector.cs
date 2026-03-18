using UnityEngine;

public class Selector
{


    public bool TrySelect(Vector2 mousePosition,out ISelectable selected)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Debug.Log("TRYING SELECT");
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<ISelectable>(out var selectable))
            {
                selected = selectable;
                Debug.Log($"I have hit the selectable {selected}");
                return true;
            }
        }

        selected = null;
        return false;
    }
}
