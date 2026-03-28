using System;
using System.IO;
using UnityEngine;

public class GameSaveLoadController 
{
    GameSaveData gameSave;
    PlayerRegistry playerRegistry;
    InventoryRegistry inventoryRegistry;

    EventBinding<SaveEvent> saveEventBinding;

    PlayerSaveData playerSaveData;
    private string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

    public GameSaveLoadController(PlayerRegistry playerRegistry,InventoryRegistry inventoryRegistry,GameSaveData gameSaveData) { 
        this.playerRegistry = playerRegistry;
        this.inventoryRegistry = inventoryRegistry;
        this.gameSave = gameSaveData;
    }

    public void Initialize() {
        saveEventBinding = new EventBinding<SaveEvent>(CaptureData);
        EventBus<SaveEvent>.Register(saveEventBinding);

    }

    public void Dispose() { 
        EventBus<SaveEvent>.Deregister(saveEventBinding);
    }

    private string GetSavePath(int slotId) =>
        Path.Combine(Application.persistentDataPath, $"save_{slotId}.json");

    private string GetMetadataPath(int slotId) =>
        Path.Combine(Application.persistentDataPath, $"save_{slotId}_meta.json");


    public void CaptureData() {
        foreach (var player in playerRegistry.Players) { 
            var saveData = new PlayerSaveData(player.Value.GameObject.transform.position,
                                                player.Value.StableID,
                                                player.Value.PlayerName,
                                                player.Value.Health.GetCurrentHealth(),
                                                player.Value.Health.GetMaxHealth(),
                                                player.Value.EntityLevel,
                                                player.Value.playerType);

            gameSave.playerSaveDatas.Add(saveData);
            if (saveData.Type == PlayerType.Human) { 
                SaveMetaData(saveData);
            }
        }

        foreach (var inventory in inventoryRegistry.All) { 
            var inventoryItems = inventory.Value.GetItems();
            var inventoryData = new InventorySaveData(inventory.Value.ownerStableID, inventoryItems, inventoryItems.Length);
            gameSave.inventorySaveDatas.Add(inventoryData);
        }

        Save(gameSave);
    }

    public void Save(GameSaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetSavePath(GameSession.SaveSlot), json);
        Debug.Log($"Saved to {GetSavePath(GameSession.SaveSlot)}");
    }

    public void SaveMetaData(PlayerSaveData humanPlayer) {
        var GameSaveMetaData = new GameSaveMetadata(GameSession.SaveSlot,humanPlayer.PlayerName,humanPlayer.Level,"GameScene","Test Zone",DateTime.UtcNow.ToString(),false);
        string json = JsonUtility.ToJson(GameSaveMetaData, true);
        File.WriteAllText(GetMetadataPath(GameSession.SaveSlot), json);
        Debug.Log($"Saved metadata to {GetMetadataPath(GameSession.SaveSlot)}");
    }

    public GameSaveData Load()
    {
        if (!File.Exists(GetSavePath(0))) 
        {
            Debug.Log("No save file found.");
            return null;
        }

        string json = File.ReadAllText(GetSavePath(GameSession.SaveSlot));
        GameSaveData data = JsonUtility.FromJson<GameSaveData>(json);
        Debug.Log($"Loaded from {GetSavePath(GameSession.SaveSlot)}");
        return data;
    }
}
