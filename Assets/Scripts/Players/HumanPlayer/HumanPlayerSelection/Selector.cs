using UnityEngine;

public class Selector
{


    public bool TrySelect(Vector2 mousePosition,out ISelectable selected)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<ISelectable>(out var selectable))
            {
                selected = selectable;
                return true;
            }
        }

        selected = null;
        return false;
    }
}
