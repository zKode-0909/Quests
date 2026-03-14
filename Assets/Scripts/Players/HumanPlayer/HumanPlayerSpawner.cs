using UnityEngine;

public class HumanPlayerSpawner
{
    Transform playerSpawnPosition;


    public void Initialize(Transform spawnPosition) { 
        playerSpawnPosition = spawnPosition;



    }

    public void Spawn(HumanPlayer player) { 
        player.gameObject.SetActive(true);
        player.transform.position = playerSpawnPosition.position;
    }
}
