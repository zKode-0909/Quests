using UnityEngine;

using UnityEngine.UIElements;


public class LoadSlot : VisualElement
{
    Button textHolder;
    GameSaveMetadata metaData;

    public LoadSlot(GameSaveMetadata metadata) {
        AddToClassList("saveSlot");

        this.metaData = metadata;

        textHolder = new Button();
        textHolder.AddToClassList("slotTextHolder");
        textHolder.clicked += () => OnSlotClicked();

        var playerNameText = new Label(metadata.playerName);
        

        var slotNumber = new Label(metadata.slotId.ToString());
        var level = new Label(metadata.level.ToString());
        var currentSceneName = new Label(metadata.currentSceneName);
        var zoneName = new Label(metadata.zoneName);
        var lastPlayed = new Label(metadata.lastPlayedUtc);
        var isEmpty = new Label(metadata.isEmpty.ToString());

        textHolder.Add(slotNumber);
        textHolder.Add(playerNameText);
        textHolder.Add(level);
        textHolder.Add(zoneName);
        textHolder.Add(lastPlayed);

        Add(textHolder);



    }

    void OnSlotClicked() {
        if (metaData.isEmpty)
        {
            GameSession.StartNewGame(metaData.slotId);
        }
        else {
            GameSession.LoadExistingGame(metaData.slotId);
        }
        
    }
}
