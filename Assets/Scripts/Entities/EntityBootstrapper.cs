using UnityEngine;


public class EntityBootstrapper : MonoBehaviour {

    public CharacterRegistry characterRegistry;

    public void BootStrap(CharacterRegistry characterRegistry)
    {


        NPCActionRunner.Initialize(characterRegistry);
    }

    private void OnDestroy()
    {
       NPCActionRunner.Shutdown();
    }

}

