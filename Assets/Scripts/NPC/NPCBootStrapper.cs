using UnityEngine;

public class NPCBootStrapper : MonoBehaviour
{
    public CharacterRegistry characterRegistry;

    public void BootStrap() { 
        characterRegistry = new CharacterRegistry();

        NPCActionRunner.Initialize(characterRegistry);
    }

    private void OnDestroy()
    {
        NPCActionRunner.Shutdown();
    }
}
