using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner
{
    List<Transform> playerSpawnPoints;

    public PlayerSpawner(List<Transform> spawnPoints) { 
        this.playerSpawnPoints = spawnPoints;
    }

    public void SpawnPlayer(Player player) {
        if (playerSpawnPoints != null) {
            int index = Random.Range(0, playerSpawnPoints.Count);
            player.transform.position = playerSpawnPoints[index].position;
            player.gameObject.SetActive(true);
        }
        
        
    }
}
