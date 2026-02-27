using UnityEngine;

public class PlayerBootStrapper : MonoBehaviour
{
    public PlayerRegistry playerRegistry;

    public void BootStrap() { 
        playerRegistry = new PlayerRegistry();
    }
}
