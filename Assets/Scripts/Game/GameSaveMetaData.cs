using UnityEngine;

[System.Serializable]
public class GameSaveMetadata
{
    public int slotId;
    public string playerName;
    public int level;
    public string currentSceneName;
    public string zoneName;
    public string lastPlayedUtc;
    public bool isEmpty;

    public GameSaveMetadata(int slot,string name,int level,string currentScene, string zone,string lastPlayedTime,bool isEmpty) { 
        this.slotId = slot;
        this.playerName = name;
        this.level = level;
        this.currentSceneName = currentScene;
        this.zoneName = zone;
        this.lastPlayedUtc = lastPlayedTime;
        this.isEmpty = isEmpty;
    }
}
