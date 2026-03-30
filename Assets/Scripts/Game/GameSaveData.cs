

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveData
{
    public List<PlayerSaveData> playerSaveDatas;
    public List<InventorySaveData> inventorySaveDatas;
    public List<QuestLogSaveData> questLogSaveDatas;

    public void Initialize() { 
        playerSaveDatas = new List<PlayerSaveData>();
        inventorySaveDatas = new List<InventorySaveData>();
        questLogSaveDatas = new List<QuestLogSaveData>();
    }
}
