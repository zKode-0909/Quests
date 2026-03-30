using UnityEngine;

public class QuestBootstrapper : MonoBehaviour
{
    [SerializeField] QuestDB dB;
    public QuestGiverService questGiverService;
    QuestLogRegistry questLogRegistry;
    InventoryRegistry inventoryRegistry;
    QuestFactory questFactory;
    public QuestGiverRegistry questGiverRegistry;
    QuestService questService;
    QuestLogController questLogController;
    QuestActionRunner questActionRunner;

    public CharacterRegistry characterRegistry;
    // Start is called once before the first execution of Update after the MonoBehaviour is created



    private void OnDestroy()
    {
        questGiverService.Dispose();
        questService.Dispose();
        questLogController.Dispose();
       
    }

    public void BootStrap(QuestLogRegistry logRegistry,InventoryRegistry inventoryRegistry,CharacterRegistry characterRegistry) {
        questGiverRegistry = new QuestGiverRegistry();

        questActionRunner = new QuestActionRunner();

        this.characterRegistry = characterRegistry;

        questFactory = new QuestFactory();
        questFactory.InitializeFactory(dB);

        questLogRegistry = logRegistry;

        questGiverService = new QuestGiverService();
        questGiverService.InitiateService(questLogRegistry, questGiverRegistry,questFactory);

        

        questLogController = new QuestLogController();
        questLogController.InitiateService(questFactory, questLogRegistry,questActionRunner,inventoryRegistry);

        questService = new QuestService();
        questService.Initialize(questFactory, questLogRegistry,questLogController);
    }

}
