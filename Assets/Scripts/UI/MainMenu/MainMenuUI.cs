using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUI
{
    private VisualElement root;
    private StyleSheet styleSheet;

    VisualElement buttonsHolder;
    Button LoadGameButton;
    Button NewGameButton;
    List<GameSaveMetadata> saveSlots;
    LoadSlots loadSlots;

    


    public MainMenuUI(VisualElement root,StyleSheet styleSheet) { 
        this.root = root;
        this.styleSheet = styleSheet;
    }

    public void Initialize() {

        saveSlots = new List<GameSaveMetadata>();

        root.Clear();
        root.styleSheets.Add(styleSheet);
        root.AddToClassList("menuDisplay");

        root.style.display = DisplayStyle.Flex;
        
        BuildMainMenu();

    }

    void BuildMainMenu() {

        saveSlots.Clear();
        saveSlots = GameSession.GetAllSlotMetadata();

        foreach (var slot in saveSlots) {
            Debug.Log($"metadata for slot: {slot.playerName},{slot.slotId},{slot.lastPlayedUtc},{slot.isEmpty}");
        }

        buttonsHolder = new VisualElement();
        buttonsHolder.AddToClassList("mainMenuButtonsHolder");

        NewGameButton = new Button();
        NewGameButton.text = "New Game";
        NewGameButton.AddToClassList("mainMenuButton");
        NewGameButton.clicked += () => OnNewGameClicked();

        LoadGameButton = new Button();
        LoadGameButton.text = "Load Game";
        LoadGameButton.AddToClassList("mainMenuButton");
        LoadGameButton.clicked += () => OnLoadGameClicked();

        buttonsHolder.Add(NewGameButton);
        buttonsHolder.Add(LoadGameButton);

        root.Add(buttonsHolder);

        loadSlots = new LoadSlots(saveSlots);
        loadSlots.Initialize();

        root.Add(loadSlots);

    }

    void OnNewGameClicked() {
        Debug.Log($"Clicked new game");
    }

    void OnLoadGameClicked() {
        Debug.Log($"Clicked load game");
        loadSlots.ShowSlots();
    }
}
