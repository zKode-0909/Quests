
using UnityEngine;


public class InteractionManager : MonoBehaviour
{
    [SerializeField] ScriptableObject inputReader;
    [SerializeField] LayerMask interactableLayerMask;
    [SerializeField] MonoBehaviour target;

    IInteractor interactor;
    IInputSource input;
    

    Vector3 rayOrigin = Vector3.zero;
    private void Awake()
    {
        interactor = (IInteractor)target;
        input = (IInputSource)inputReader;
        if (interactor == null) {
            Debug.Log("failed getting target");
        }

        if (input == null)
        {
            Debug.Log("failed getting input");
        }
        //target = this.gameObject.transform;
        input.InteractEvent += OnInteract;
    }


    void OnInteract() {
        Debug.Log("Interacting");


        // Does the ray intersect any objects excluding the player layer
        Vector3 fwd = interactor.Transform.TransformDirection(Vector3.forward);
        
        if (Physics.Raycast(rayOrigin, fwd, out RaycastHit hit, 10, interactableLayerMask)) {
            Debug.Log("hit layer!");
            var interactable = hit.collider.GetComponentInParent<IInteractable>();
            if (interactable != null)
            {
                Debug.Log("This object implements IInteractable!");
                interactable.HandleInteract(interactor);
            }
        }
            

    }

    private void Update()
    {
        rayOrigin = new Vector3(interactor.Transform.position.x, interactor.Transform.position.y + 1.5f, interactor.Transform.position.z);
    }


}
