using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] UIDocument uiDocument;
    [SerializeField] StyleSheet mainMenuStyleSheet;

    private VisualElement mainMenuRoot;

    MainMenuUI uiMenu;

    VisualElement root;


    private void Start()
    {

        root = GetComponent<UIDocument>().rootVisualElement;
        root.Clear();

        mainMenuRoot = new VisualElement();

        uiMenu = new MainMenuUI(mainMenuRoot,mainMenuStyleSheet);
        uiMenu.Initialize();

        root.Add(mainMenuRoot);

    }
}
