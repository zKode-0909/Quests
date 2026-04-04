using UnityEngine;
using UnityEngine.UIElements;

[DefaultExecutionOrder(-10000)]
public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] QuestBootstrapper questBootstrapper;
    [SerializeField] EntityBootstrapper entityBootstrapper;
    [SerializeField] PlayersBootstrapper PlayersBootstrapper;
    [SerializeField] InventoryBootstrapper InventoryBootstrapper;
    [SerializeField] ItemBootstrapper ItemBootstrapper;
    [SerializeField] ItemDB itemDatabase;
    [SerializeField] PartyBootstrapper PartyBootstrapper;
    [SerializeField] SimulationBootstrapper SimulationBootstrapper;
   
  
    QuestLogRegistry logRegistry;
    InventoryRegistry inventoryRegistry;
    ItemFactory itemFactory;
    InventoryFactory inventoryFactory;
    PlayerRegistry playerRegistry;
    CharacterRegistry characterRegistry;


    GameSaveLoadController gameSaveLoadController;
    GameSaveData gameSaveData;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

        playerRegistry = new PlayerRegistry();
       

        inventoryRegistry = new InventoryRegistry();
        inventoryRegistry.InitializeRegistry();

        characterRegistry = new CharacterRegistry();
     

        logRegistry = new QuestLogRegistry();
        logRegistry.InitializeRegistry();

        gameSaveData = new GameSaveData();
        gameSaveData.Initialize();



        itemFactory = new ItemFactory();
        itemFactory.InitializeFactory(itemDatabase);

        inventoryFactory = new InventoryFactory(itemFactory);





        gameSaveLoadController = new GameSaveLoadController(playerRegistry,inventoryRegistry,logRegistry,gameSaveData);
        gameSaveLoadController.Initialize();

        if (!GameSession.NewGame) {
            gameSaveData = gameSaveLoadController.Load();

            foreach (var data in gameSaveData.playerSaveDatas)
            {

                Debug.Log($"Logging data for: {data.StableID}");
                Debug.Log($"Level: {data.Level}");
                Debug.Log($"Name: {data.PlayerName}");
                Debug.Log($"Position: {data.Position}");
                Debug.Log($"Current Health: {data.CurrentHealth}");
                Debug.Log($"Max Health: {data.MaxHealth}");
            }
        }

        



        

        
        
       // PreWarmLogRegistry();
        questBootstrapper.BootStrap(logRegistry,inventoryRegistry,characterRegistry);
        //NPCBootstrapper.BootStrap();
        entityBootstrapper.BootStrap(characterRegistry);
        PlayersBootstrapper.Bootstrap(playerRegistry,gameSaveData.playerSaveDatas,GameSession.NewGame);
        InventoryBootstrapper.Bootstrap(inventoryRegistry,itemFactory,inventoryFactory,gameSaveData.inventorySaveDatas);
        ItemBootstrapper.Bootstrap(itemDatabase,itemFactory);
        SimulationBootstrapper.Bootstrap();
        PartyBootstrapper.Bootstrap();
        

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
