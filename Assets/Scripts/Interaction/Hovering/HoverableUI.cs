using UnityEngine;
using UnityEngine.UIElements;

public class HoverableUI : MonoBehaviour
{
    [SerializeField] UIDocument document;
    [SerializeField] StyleSheet styleSheet;

    VisualElement root;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        root = document.rootVisualElement;
        root.Clear();

        root.styleSheets.Add(styleSheet);

        root.AddToClassList("hoverableScreen");

        BuildHoverableUI();

        root.style.display = DisplayStyle.None;
        


    }

    public void ShowHoverableUI() {
        root.style.display = DisplayStyle.Flex;
        
    }

    public void HideHoverableUI() { 
        root.style.display = DisplayStyle.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BuildHoverableUI() {
        var text = new Label();
        text.text = "INTERACT";

        root.Add(text);
    }
}
