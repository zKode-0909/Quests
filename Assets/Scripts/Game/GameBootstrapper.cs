using UnityEngine;
using UnityEngine.UIElements;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] QuestBootstrapper questBootstrapper;
    QuestLogRegistry logRegistry;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        logRegistry = new QuestLogRegistry();
        PreWarmLogRegistry();
        questBootstrapper.BootStrap(logRegistry);
    }

    void PreWarmLogRegistry() {
        var players = FindObjectsByType<Player>(FindObjectsSortMode.None);

        foreach(var player in players)
        {
            logRegistry.TryCreate(player.EntityRuntimeID);
        }
    }



}
