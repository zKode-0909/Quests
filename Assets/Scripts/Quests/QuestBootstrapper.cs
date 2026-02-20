using UnityEngine;

public class QuestBootstrapper : MonoBehaviour
{
    [SerializeField] QuestDB dB;
    public QuestGiverService questGiverService;
    QuestLogRegistry questLogRegistry;
    QuestFactory questFactory;
    public QuestGiverRegistry questGiverRegistry;
    QuestService questService;
    QuestLogController questLogController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created



    private void OnDestroy()
    {
        questGiverService.Dispose();
        questService.Dispose();
        questLogController.Dispose();
       
    }

    public void BootStrap(QuestLogRegistry logRegistry) {
        questGiverRegistry = new QuestGiverRegistry();

        questFactory = new QuestFactory();
        questFactory.InitializeFactory(dB);

        questLogRegistry = logRegistry;

        questGiverService = new QuestGiverService();
        questGiverService.InitiateService(questLogRegistry, questGiverRegistry);

        questService = new QuestService();
        questService.Initialize(questFactory, questLogRegistry);

        questLogController = new QuestLogController();
        questLogController.InitiateService(questFactory, questLogRegistry);
    }

}
