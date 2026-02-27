using UnityEngine;

public class NPCBootStrapper : MonoBehaviour
{
    public CharacterRegistry characterRegistry;

    public void BootStrap() { 
        characterRegistry = new CharacterRegistry();
    }
}
