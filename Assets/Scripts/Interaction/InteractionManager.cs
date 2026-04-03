
using UnityEngine;


public class InteractionManager : MonoBehaviour
{
    [SerializeField] ScriptableObject inputReader;
    [SerializeField] LayerMask interactableLayerMask;
    MonoBehaviour target;

    private IInteractor interactor;
    private IInputSource input;


    Vector3 origin = Vector3.zero;
    private void Awake()
    {
        interactor = target as IInteractor;
        input = inputReader as IInputSource;

        if (interactor == null) Debug.LogError("Target must implement IInteractor", this);
        if (input == null) Debug.LogError("InputReader must implement IInputSource", this);

        
    }
    /*
    void OnEnable() => input.InteractEvent += OnInteract;
    void OnDisable() { if (input != null) input.InteractEvent -= OnInteract; }


    void OnInteract() {



        // Does the ray intersect any objects excluding the player layer
        Vector3 fwd = interactor.GameObject.transform.TransformDirection(Vector3.forward);
        origin = interactor.GameObject.transform.position + Vector3.up * 1.5f;
       if (Physics.Raycast(origin, fwd, out RaycastHit hit, 10, interactableLayerMask)) {

            var interactable = hit.collider.GetComponentInParent<IInteractable>();
            if (interactable != null)
            {
                interactable.HandleInteract(interactor);
            }
        }
            

    }
    */
    public void SetTarget(MonoBehaviour target) { 
        this.target = target;
    }

  



}
