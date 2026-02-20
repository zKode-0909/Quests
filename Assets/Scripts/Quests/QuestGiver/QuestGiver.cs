
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour,IInteractable,IQuestGiver
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
        var level = ((ILeveller)interactor).Level;
        
        Debug.Log("Interacted with QuestGiver");
        EventBus<RequestOpenQuestGiverUI>.Raise(new RequestOpenQuestGiverUI(interactor.EntityRuntimeID,entityRuntimeId,level));
   
    }

    public void SetQuestIcon(QuestDisplayIcon icon) {
        Debug.Log("I am setting my quest icon");
        questMarker.SetMarker(icon);
    }
    private void Awake()
    {
        entityRuntimeId = GetEntityId();
        Debug.Log(bootstrapper.questGiverRegistry.TryRegister(this));//.questGiverRegistry//.TryRegister(this);
    }
    private void Start()
    {
        

        questGiverText = "Yo nigga, you tryna get some quests? Take a look, tell me what ya think....";
        questGiverID = new StringID("questGiver", "testName", "testZone", ".001").id;
        
        //currentQuest = quests[0].CreateQuest(this);
    }

   


}
