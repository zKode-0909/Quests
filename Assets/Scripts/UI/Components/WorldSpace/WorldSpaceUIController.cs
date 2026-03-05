using UnityEngine;
using UnityEngine.UIElements;

public class WorldSpaceUIController : MonoBehaviour
{
    [SerializeField] UIDocument document;
    [SerializeField] StyleSheet styleSheet;


    VisualElement root;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        root = document.rootVisualElement;
       // root.Clear();
        root.styleSheets.Add(styleSheet);


        transform.position = new Vector3(0, 2, 5);
        transform.rotation = Quaternion.identity;

        root.style.width = 500;
        root.style.height = 500;

        root.style.backgroundColor = Color.white;

    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
        //if (Camera.main == null) return;

        // Make the panel face the camera
        // var cam = Camera.main.transform;
        //  transform.rotation = Quaternion.LookRotation(transform.position - cam.position, cam.up);
    }
}
