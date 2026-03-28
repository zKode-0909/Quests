using System.Collections.Generic;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameSession
{
    public static int SaveSlot { get; private set; } = -1;
    public static bool NewGame { get; private set; }

    public static void StartNewGame(int slotId)
    {
        SaveSlot = slotId;
        NewGame = true;

        SceneManager.LoadScene("GameOverworldScene");
    }

    public static void LoadExistingGame(int slotId)
    {
        SaveSlot = slotId;
        NewGame = false;

        SceneManager.LoadScene("GameOverworldScene");
    }



    private static string GetMetadataPath(int slotId) =>
        Path.Combine(Application.persistentDataPath, $"save_{slotId}_meta.json");
    public static List<GameSaveMetadata> GetAllSlotMetadata()
    {
        List<GameSaveMetadata> slots = new List<GameSaveMetadata>();

        for (int i = 0; i < 5; i++)
        {
            var metadata = LoadMetadata(i);

            if (metadata == null)
            {
                metadata = new GameSaveMetadata(
                    i,
                    "",
                    0,
                    "",
                    "",
                    "",
                    true
                );
            }

            slots.Add(metadata);
        }

        return slots;
    }

    public static GameSaveMetadata LoadMetadata(int slotId)
    {
        string path = GetMetadataPath(slotId);

        if (!File.Exists(path))
            return null;

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<GameSaveMetadata>(json);
    }
}
