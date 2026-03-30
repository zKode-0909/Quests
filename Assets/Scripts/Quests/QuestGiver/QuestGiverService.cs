using System.Collections.Generic;
using UnityEngine;

public class QuestGiverService 
{
    private EventBinding<RequestOpenQuestGiverUI> openReqBinding;
    //private EventBinding<RequestAcceptQuest> acceptReqBinding;
    private EventBinding<RequestQuestGiverIconDisplay> displayIconReqBinding;
    
    private EventBinding<RequestScanQuestGivers> scanGiversReqBinding;
    private QuestLogRegistry logRegistry;
    QuestFactory questFactory;
    QuestGiverRegistry giverRegistry;


    int detectionRadius;
    LayerMask layerMask;

    



    public void InitiateService(QuestLogRegistry logs,QuestGiverRegistry givers, QuestFactory factory)
    {
        openReqBinding = new EventBinding<RequestOpenQuestGiverUI>(OnOpenRequested);
        displayIconReqBinding = new EventBinding<RequestQuestGiverIconDisplay>(OnQuestGiverIconDisplayRequested);
        


        logRegistry = logs;
        giverRegistry = givers;
        questFactory = factory;

        EventBus<RequestOpenQuestGiverUI>.Register(openReqBinding);
        EventBus<RequestQuestGiverIconDisplay>.Register(displayIconReqBinding);


        
        
    }



    public void Dispose() { 
        EventBus<RequestOpenQuestGiverUI>.Deregister(openReqBinding);
       // EventBus<RequestAcceptQuest>.Deregister(acceptReqBinding);
        EventBus<RequestQuestGiverIconDisplay>.Deregister(displayIconReqBinding);



        logRegistry = null;
    }

   



    void OnQuestGiverIconDisplayRequested(RequestQuestGiverIconDisplay evt) {
        Debug.Log($"quest giver's id is: {evt.QuestGiverEntityStableID}");
        if (giverRegistry.TryGet(evt.QuestGiverEntityStableID,out var giver)) {
           
            var questGiver = giver;
            var questLog = logRegistry.GetOrCreate(evt.QuesterEntityStableID,true);
            Debug.Log($"checking for icon, questlog is: {questLog} and questgiver is {giver}");
            if (questGiver != null && questLog != null)
            {
                var icon = QuestUtils.DetermineIcon(questGiver, questLog, evt.QuesterLevel);

                questGiver.SetQuestIcon(icon);

            }
        }
        
        
    }
    /*
    void OnQuestGiverScanRequested(RequestScanQuestGivers evt) { 
        questGiverScanner.ForceRescanNearby(evt.EntityLevel,evt.EntityRuntimeID)
    }*/

    

    public bool TryRegisterQuestGiver(QuestGiver questGiver) { 
        return giverRegistry.TryRegister(questGiver);
    }

    void OnOpenRequested(RequestOpenQuestGiverUI evt) {

        if (giverRegistry.TryGet(evt.QuestGiverEntityId, out var giver)) {
            var questLog = logRegistry.GetOrCreate(evt.QuesterEntityId,true);
            var questItems = BuildQuestItems(giver, questLog, evt.QuesterLevel);
   
            EventBus<OpenQuestGiverUI>.Raise(new OpenQuestGiverUI(questItems, "TestGiverName", "TestQuesterName", giver.StableID, evt.QuesterEntityId,evt.QuesterLevel));
        }    
    }

   




    List<QuestGiverQuestUIItem> BuildQuestItems(QuestGiver questGiver,QuestLog questLog, int level) {

        var items = new List<QuestGiverQuestUIItem>();    
        foreach (var quest in questGiver.Quests) {
            var status = QuestUtils.DetermineQuestStatus(quest, questLog,level);
            if (status != QuestStatus.None) {
                items.Add(new QuestGiverQuestUIItem(quest.QuestName, quest.QuestDescription, questGiver.EntityRuntimeID, quest.ID, $"{status}"));
            }
        }

        return items;
    }
}
