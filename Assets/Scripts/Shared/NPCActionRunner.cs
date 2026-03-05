using UnityEngine;

public static class NPCActionRunner
{
    static CharacterRegistry characterRegistry;

    public static void Initialize(CharacterRegistry registry) { 
        characterRegistry = registry;
    }

    public static void Shutdown() { 
        characterRegistry = null;
    }

    public static void NPCSayAction(string entityStableID,string words) {
        if (characterRegistry.TryGet(entityStableID,out var npc)) {
            npc.Say(words);
        }
    }


}
