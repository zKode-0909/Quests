using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestHandoutView : MonoBehaviour
{
    [SerializeField] UIDocument document;
    [SerializeField] StyleSheet styleSheet;
    //[SerializeField] QuestService qService;

    private static EventBinding<OpenQuestGiverUI> openBinding;

    VisualElement root;
    SelectedQuestView selectedQuestView;
    QuestGiverView questGiverView;
    IReadOnlyList<QuestUIItem> questsToDisplay;
    string currentQuester;
   // IReadOnlyList<QuestSettings> allQuests;
    QuestUIItem currentQuest;
     string questGiverName;

    int questerID;
    int giverID;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        openBinding = new EventBinding<OpenQuestGiverUI>(HandleOpenUI);
        EventBus<OpenQuestGiverUI>.Register(openBinding);
  


        root = document.rootVisualElement;
        root.Clear();
 //      .. allQuests = new List<QuestSettings>();
        root.styleSheets.Add(styleSheet);

        root.AddToClassList("questHandoutDisplay");
        BuildQuestGiverDisplay();
        BuildQuestHandoutDisplay();
        questGiverView.QuestSelected += ShowQuestDisplay;
        questGiverView.CloseView += CloseDisplay;
        selectedQuestView.DeclinedQuest += BackToQuestGiver;
    //    selectedQuestView.AcceptedQuest += HandleAcceptQuest;
        
        CloseDisplay();
        //CloseQuestDisplay();
    }


  

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleOpenUI(OpenQuestGiverUI evt) {
        Debug.Log($"We opening UI for {evt.questGiverName} and quester: {evt.questerName}. There are {evt.questsToShow.Count} quests");
        ShowQuestGiverDisplay(evt.questsToShow,evt.questerName,evt.questGiverName,evt.questerID,evt.questGiverID);
    }


    public void BuildQuestHandoutDisplay() {

        selectedQuestView = new SelectedQuestView();
        root.Add(selectedQuestView);

    }

    public void BuildQuestGiverDisplay() {
        questGiverView = new QuestGiverView(this);
        root.Add(questGiverView);
    }
    
    public void ShowQuestDisplay(QuestUIItem quest) {
        root.style.display = DisplayStyle.Flex;
        selectedQuestView.ShowSelectedQuest(quest);
        questGiverView.CloseQuestGiverView();
        
        currentQuest = quest;
        

    }
    
    public void BackToQuestGiver() {
        EventBus<RequestOpenQuestGiverUI>.Raise(new RequestOpenQuestGiverUI(questerID,giverID));
        //ShowQuestGiverDisplay(allQuests, currentInteractor, questGiver);
    }
    
    public void ShowQuestGiverDisplay(IReadOnlyList<QuestUIItem> quests,string questerName,string giverName,int questerID,int giverID) {
        root.style.display= DisplayStyle.Flex;
        currentQuester = questerName;
        //allQuests.Clear();
        questsToDisplay = quests;
        questGiverName = giverName;
        this.questerID = questerID;
        this.giverID = giverID;
        selectedQuestView.HideSelectedQuest();

        questGiverView.ShowQuestGiverView(questsToDisplay, currentQuester, questGiverName);
        
       
        

    }

    public void CloseDisplay() {
        root.style.display = DisplayStyle.None;
        selectedQuestView.HideSelectedQuest();
        questGiverView.CloseQuestGiverView();

        currentQuester = null;
       
       // currentQuest = null;
    }


    
    public void HandleAcceptQuest() {
        EventBus<RequestAcceptQuest>.Raise(new RequestAcceptQuest(questerID,giverID,currentQuest.questID));
        /*
        if (currentInteractor.GameObject.TryGetComponent<IQuester>(out var quester)) {
            Debug.Log($"accepted quest from questGiver, quester is {quester}, currentQuest is {currentQuest}");
            var newQuest = currentQuest.CreateQuest(currentQuest.ID);
            Debug.Log($"new quest ID: {currentQuest.ID}   newQuest: {newQuest}");
            qService.TryAcceptQuest(quester, newQuest);
        }*/

        BackToQuestGiver();
        
    }
    
}
