using UnityEngine;

public class HoverManager : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] LayerMask interactableLayerMask;
    [SerializeField] HoverableUI hoverableUI;
    Vector3 rayOrigin = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rayOrigin = new Vector3(target.position.x, target.position.y + 1.5f, target.position.z);
        CheckForHoverable();
    }

    void CheckForHoverable() {

        Vector3 fwd = target.TransformDirection(Vector3.forward);

        if (Physics.Raycast(rayOrigin, fwd, out RaycastHit hit, 10, interactableLayerMask))
        {
            var hoverable = hit.collider.GetComponentInParent<IInteractable>();

           // Debug.Log("hit layer!");
            if (hoverable != null)
            {
                Debug.Log("Hovering IInteractable");
                hoverableUI.ShowHoverableUI();
                return;
                //interactable.HandleInteract();
            }
        }

        hoverableUI.HideHoverableUI();
    }
}
