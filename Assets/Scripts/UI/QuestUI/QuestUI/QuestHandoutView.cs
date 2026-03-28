using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestHandoutView : VisualElement
{
   
    //[SerializeField] QuestService qService;

    private static EventBinding<OpenQuestGiverUI> openBinding;

    VisualElement root;
    StyleSheet styleSheet;
    SelectedQuestView selectedQuestView;
    QuestGiverView questGiverView;
    IReadOnlyList<QuestUIItem> questsToDisplay;
    string currentQuester;
   // IReadOnlyList<QuestSettings> allQuests;
    QuestUIItem currentQuest;
     string questGiverName;
    int questerLevel;
    string questerID;
    string giverID;
    bool open;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public QuestHandoutView(VisualElement root,StyleSheet styleSheet) { 
        this.root = root;
        this.styleSheet = styleSheet;
    }

    public void Initialize() {

        open = false;

        root.Clear();
        root.styleSheets.Add(styleSheet);
        root.AddToClassList("questHandoutDisplay");

        BuildQuestGiverDisplay();
        BuildQuestHandoutDisplay();

        questGiverView.QuestSelected += ShowQuestDisplay;
        questGiverView.CloseView += CloseDisplay;
        selectedQuestView.DeclinedQuest += BackToQuestGiver;
        selectedQuestView.AcceptedQuest += HandleAcceptQuest;

        CloseDisplay();
    }

    public void Dispose() {
        root.Clear();
    }

   

    public void HandleOpenUI(OpenQuestGiverUI evt) {
        Debug.Log($"Opening quest giver display!");
        if (!open) {
            Debug.Log($"Showing quest giver display!");
            open = true;
            ShowQuestGiverDisplay(evt.questsToShow, evt.questerName, evt.questGiverName, evt.questerID, evt.questGiverID, evt.questerLevel);
        }
        
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
        open = false;
        EventBus<RequestOpenQuestGiverUI>.Raise(new RequestOpenQuestGiverUI(questerID,giverID,questerLevel));
        
        //ShowQuestGiverDisplay(allQuests, currentInteractor, questGiver);
    }
    
    public void ShowQuestGiverDisplay(IReadOnlyList<QuestUIItem> quests,string questerName,string giverName,string questerID,string giverID,int questerLevel) {
        root.style.display= DisplayStyle.Flex;
        currentQuester = questerName;
        //allQuests.Clear();
        questsToDisplay = quests;
        questGiverName = giverName;
        this.questerID = questerID;
        this.giverID = giverID;
        selectedQuestView.HideSelectedQuest();
        this.questerLevel = questerLevel;
        questGiverView.ShowQuestGiverView(questsToDisplay, currentQuester, questGiverName);
        
       
        

    }

    public void CloseDisplay() {
       
            root.style.display = DisplayStyle.None;
            selectedQuestView.HideSelectedQuest();
            questGiverView.CloseQuestGiverView();

            currentQuester = null;

            open = false;
            // currentQuest = null;
        

    }


    
    public void HandleAcceptQuest() {
        EventBus<RequestAcceptQuest>.Raise(new RequestAcceptQuest(questerID,giverID,currentQuest.questID));
        BackToQuestGiver();
        
    }
    
}
