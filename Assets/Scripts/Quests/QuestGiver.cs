
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour,IInteractable,IEntity
{
    //[SerializeField] QuestHandoutView view;
    [SerializeField] List<QuestSettings> quests = new();
    public IReadOnlyList<QuestSettings> Quests => quests;

    public GameObject GameObject => this.gameObject;

    public int EntityRuntimeID => entityRuntimeId;

    int entityRuntimeId = 2;

    [SerializeField] QuestMarkerMB questMarker;
    bool showingDisplay;
   // Quest currentQuest;
    public string questGiverID;
    public string questGiverText;

    public void HandleInteract(IInteractor interactor)
    {
        Debug.Log("Interacted with QuestGiver");

        view.ShowQuestGiverDisplay(Quests,interactor,this);


        
        
    }

    public void SetQuestIcon(QuestDisplayIcon icon) {
        Debug.Log("I am setting my quest icon");
        questMarker.SetMarker(icon);
    }

    private void Awake()
    {
        questGiverText = "Yo nigga, you tryna get some quests? Take a look, tell me what ya think....";
        questGiverID = new StringID("questGiver", "testName", "testZone", ".001").id;
        //currentQuest = quests[0].CreateQuest(this);
    }




}
