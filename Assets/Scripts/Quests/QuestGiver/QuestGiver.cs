
using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-9999)]
public class QuestGiver : MonoBehaviour,IInteractable,ICharacter,ISelectable
{
    //[SerializeField] QuestHandoutView view;
    
    [SerializeField] QuestBootstrapper bootstrapper;
    QuestGiverService service;
    [SerializeField] List<QuestSettings> quests = new();
    public IReadOnlyList<QuestSettings> Quests => quests;

    public GameObject GameObject => this.gameObject;

    public int EntityRuntimeID => entityRuntimeId;


    public int EntityLevel => 5;

    string stableID;

    SelectableType selectableType = SelectableType.NPC;

    public SelectableType SelectableType => selectableType;
    public string StableID => stableID;
  
    public int entityRuntimeId;

    [SerializeField] QuestMarkerMB questMarker;
    bool showingDisplay;
   // Quest currentQuest;
    public string questGiverID;
    public string questGiverText;

    public event Action<int> healthChangedEvent;

    public void HandleInteract(IInteractor interactor)
    {
        var level = ((IEntity)interactor).EntityLevel;
        
        Debug.Log($"Interacted with QuestGiver with entityruntimeid of {entityRuntimeId}");
        EventBus<RequestOpenQuestGiverUI>.Raise(new RequestOpenQuestGiverUI(interactor.StableID,StableID,level));
   
    }

    public void SetQuestIcon(QuestDisplayIcon icon) {
        questMarker.SetMarker(icon);
    }
    private void Awake()
    {
        stableID = "<TESTERGUNNARSON>";
        entityRuntimeId = RuntimeIDGenerator.GetNext();
        Debug.Log($"I {gameObject.name} am registering with id {entityRuntimeId}");
        bootstrapper.characterRegistry.Register(this);
        //entityRuntimeId = GetComponentInParent<IEntity>().EntityRuntimeID;//GetEntityId();
        //Debug.Log(bootstrapper.questGiverRegistry);
        bootstrapper.questGiverRegistry.TryRegister(this);//.questGiverRegistry//.TryRegister(this); 
    }
    private void Start()
    {

        
        questGiverText = "Yo nigga, you tryna get some quests? Take a look, tell me what ya think....";
        questGiverID = new StringID("questGiver", "testName", "testZone", ".001").id;
        
        //currentQuest = quests[0].CreateQuest(this);
    }

    public void Say(string words) {
        Debug.Log(words);
    }

    public SelectableData SendSelectionData()
    {
        return new SelectableData();
    }

    public void UpdatePartyStatus(bool status)
    {
        Debug.Log("Fuck this");
    }
}
