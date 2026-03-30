using UnityEngine;

public class NPCBootStrapper : MonoBehaviour
{
    public CharacterRegistry characterRegistry;

    public void BootStrap(CharacterRegistry characterRegistry) { 
        

        //NPCActionRunner.Initialize(characterRegistry);
    }

    private void OnDestroy()
    {
       // NPCActionRunner.Shutdown();
    }
}
