using UnityEngine;
using UnityEngine.UIElements;

[DefaultExecutionOrder(-10000)]
public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] QuestBootstrapper questBootstrapper;
    [SerializeField] NPCBootStrapper NPCBootstrapper;
    [SerializeField] PlayerBootStrapper PlayerBootStrapper;
    [SerializeField] InventoryBootstrapper InventoryBootstrapper;
    [SerializeField] ItemBootstrapper ItemBootstrapper;
    [SerializeField] ItemDB itemDatabase;
  
    QuestLogRegistry logRegistry;
    InventoryRegistry inventoryRegistry;
    ItemFactory itemFactory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
   
        itemFactory = new ItemFactory();

        inventoryRegistry = new InventoryRegistry();
        inventoryRegistry.InitializeRegistry();
        logRegistry = new QuestLogRegistry();
        logRegistry.InitializeRegistry();
       // PreWarmLogRegistry();
        questBootstrapper.BootStrap(logRegistry);
        NPCBootstrapper.BootStrap();
        PlayerBootStrapper.BootStrap();
        InventoryBootstrapper.Bootstrap(inventoryRegistry,itemFactory);
        ItemBootstrapper.Bootstrap(itemDatabase,itemFactory);
    }
    /*
    void PreWarmLogRegistry() {
        var players = FindObjectsByType<Player>(FindObjectsSortMode.None);

        foreach(var player in players)
        {
            logRegistry.TryCreate(player.EntityRuntimeID);
            inventoryRegistry.TryCreate(player.EntityRuntimeID);
        }
    }*/



}
