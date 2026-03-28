using UnityEngine;
using UnityEngine.UIElements;

public class InGameMenu
{
    VisualElement root;
    StyleSheet style;

    Button saveButton;
    VisualElement buttonsHolder;


    public InGameMenu(VisualElement root,StyleSheet styleSheet) { 
        this.root = root;
        

        this.style = styleSheet;
    }

    public void Initialize() {

        root.Clear();
        root.styleSheets.Add(style);
        root.AddToClassList("menuDisplay");

        BuildMenu();

        root.style.display = DisplayStyle.None;
    }

    void BuildMenu() {

        buttonsHolder = new VisualElement();
        buttonsHolder.AddToClassList("buttonsHolder");

        saveButton = new Button();
        saveButton.text = "Save Game";
        saveButton.AddToClassList("menuButton");
        saveButton.clicked += () => OnSaveClicked();

        buttonsHolder.Add(saveButton);

        root.Add(buttonsHolder);
    }

    public void ShowMenu() {
        root.style.display = DisplayStyle.Flex;
    }

    public void HideMenu() {
        root.style.display = DisplayStyle.None;
    }

    void OnSaveClicked() {
        Debug.Log("Clicked save Button");
        EventBus<SaveEvent>.Raise(new SaveEvent());
    }



}
