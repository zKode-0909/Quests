
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-9999)]
public class QuestGiver : MonoBehaviour,IInteractable
{
    //[SerializeField] QuestHandoutView view;
    [SerializeField] QuestBootstrapper bootstrapper;
    QuestGiverService service;
    [SerializeField] List<QuestSettings> quests = new();
    public IReadOnlyList<QuestSettings> Quests => quests;

    public GameObject GameObject => this.gameObject;

    public int EntityRuntimeID => entityRuntimeId;


    public int EntityLevel => 5;
    


  
    public int entityRuntimeId;

    [SerializeField] QuestMarkerMB questMarker;
    bool showingDisplay;
   // Quest currentQuest;
    public string questGiverID;
    public string questGiverText;

    public void HandleInteract(IInteractor interactor)
    {
        var level = ((IEntity)interactor).EntityLevel;
        
        Debug.Log($"Interacted with QuestGiver with entityruntimeid of {entityRuntimeId}");
        EventBus<RequestOpenQuestGiverUI>.Raise(new RequestOpenQuestGiverUI(interactor.EntityRuntimeID,entityRuntimeId,level));
   
    }

    public void SetQuestIcon(QuestDisplayIcon icon) {
        questMarker.SetMarker(icon);
    }
    private void Awake()
    {
        entityRuntimeId = GetComponentInParent<IEntity>().EntityRuntimeID;//GetEntityId();
        //Debug.Log(bootstrapper.questGiverRegistry);
        bootstrapper.questGiverRegistry.TryRegister(this);//.questGiverRegistry//.TryRegister(this); 
    }
    private void Start()
    {

        
        questGiverText = "Yo nigga, you tryna get some quests? Take a look, tell me what ya think....";
        questGiverID = new StringID("questGiver", "testName", "testZone", ".001").id;
        
        //currentQuest = quests[0].CreateQuest(this);
    }

   


}
