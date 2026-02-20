using System.Collections.Generic;
using UnityEngine;

public class QuestGiverService 
{
    private EventBinding<RequestOpenQuestGiverUI> openReqBinding;
    //private EventBinding<RequestAcceptQuest> acceptReqBinding;
    private EventBinding<RequestQuestGiverIconDisplay> displayIconReqBinding;
    
    private QuestLogRegistry logRegistry;
    
    QuestGiverRegistry giverRegistry;

    



    public void InitiateService(QuestLogRegistry logs,QuestGiverRegistry givers)
    {
        openReqBinding = new EventBinding<RequestOpenQuestGiverUI>(OnOpenRequested);
        displayIconReqBinding = new EventBinding<RequestQuestGiverIconDisplay>(OnQuestGiverIconDisplayRequested);
        
        Debug.Log("service intiated");

        logRegistry = logs;
        giverRegistry = givers;

        EventBus<RequestOpenQuestGiverUI>.Register(openReqBinding);
        //EventBus<RequestAcceptQuest>.Register(acceptReqBinding);
        EventBus<RequestQuestGiverIconDisplay>.Register(displayIconReqBinding);


        
        
    }



    public void Dispose() { 
        EventBus<RequestOpenQuestGiverUI>.Deregister(openReqBinding);
       // EventBus<RequestAcceptQuest>.Deregister(acceptReqBinding);
        EventBus<RequestQuestGiverIconDisplay>.Deregister(displayIconReqBinding);
        

        logRegistry = null;
    }

    

    void OnQuestGiverIconDisplayRequested(RequestQuestGiverIconDisplay evt) {

        Debug.Log("about to try and display icon");
        if (giverRegistry.TryGet(evt.QuestGiverEntityRuntimeID,out var giver)) {
            Debug.Log("trying to display icon");
            var questGiver = giver;
            var questLog = logRegistry.GetOrCreate(evt.QuesterEntityRuntimeID);

            if (questGiver != null && questLog != null)
            {
                var icon = QuestUtils.DetermineIcon(questGiver, questLog, evt.QuesterLevel);

                questGiver.SetQuestIcon(icon);

            }
        }
        
        
    }

    

    public bool TryRegisterQuestGiver(QuestGiver questGiver) { 
        return giverRegistry.TryRegister(questGiver);
    }

    void OnOpenRequested(RequestOpenQuestGiverUI evt) {
        Debug.Log("Requested open");
        if (giverRegistry.TryGet(evt.QuestGiverEntityId, out var giver)) {
            var questLog = logRegistry.GetOrCreate(evt.QuesterEntityId);
            var questItems = BuildQuestItems(giver, questLog, evt.QuesterLevel);
   
            EventBus<OpenQuestGiverUI>.Raise(new OpenQuestGiverUI(questItems, "TestGiverName", "TestQuesterName", giver.EntityRuntimeID, evt.QuesterEntityId,evt.QuesterLevel));
        }    
    }

    


    List<QuestUIItem> BuildQuestItems(IQuestGiver questGiver,QuestLog questLog, int level) {

        var items = new List<QuestUIItem>();    
        foreach (var quest in questGiver.Quests) {
            var status = QuestUtils.DetermineQuestStatus(quest, questLog,level);
            if (status != QuestStatus.None) {
                items.Add(new QuestUIItem(quest.QuestName, quest.QuestDescription, questGiver.EntityRuntimeID, quest.ID, status));
            }
        }

        return items;
    }
}
