using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestHandoutView : MonoBehaviour
{
    [SerializeField] UIDocument document;
    [SerializeField] StyleSheet styleSheet;
    [SerializeField] QuestService qService;

    VisualElement root;
    SelectedQuestView selectedQuestView;
    QuestGiverView questGiverView;
   
    IInteractor currentInteractor;
    IReadOnlyList<QuestSettings> allQuests;
    QuestSettings currentQuest;
    QuestGiver questGiver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        root = document.rootVisualElement;
        root.Clear();
        allQuests = new List<QuestSettings>();
        root.styleSheets.Add(styleSheet);

        root.AddToClassList("questHandoutDisplay");
        BuildQuestGiverDisplay();
        BuildQuestHandoutDisplay();
        questGiverView.QuestSelected += ShowQuestDisplay;
        questGiverView.CloseView += CloseDisplay;
        selectedQuestView.DeclinedQuest += BackToQuestGiver;
        selectedQuestView.AcceptedQuest += HandleAcceptQuest;
        
        CloseDisplay();
        //CloseQuestDisplay();
    }


  

    // Update is called once per frame
    void Update()
    {
        
    }


    public void BuildQuestHandoutDisplay() {

        selectedQuestView = new SelectedQuestView();
        root.Add(selectedQuestView);

    }

    public void BuildQuestGiverDisplay() {
        questGiverView = new QuestGiverView(this);
        root.Add(questGiverView);
    }

    public void ShowQuestDisplay(QuestSettings quest) {
        root.style.display = DisplayStyle.Flex;
        selectedQuestView.ShowSelectedQuest(quest);
        questGiverView.CloseQuestGiverView();
        
        currentQuest = quest;
        

    }

    public void BackToQuestGiver() {
        ShowQuestGiverDisplay(allQuests, currentInteractor, questGiver);
    }

    public void ShowQuestGiverDisplay(IReadOnlyList<QuestSettings> quests,IInteractor interactor,QuestGiver giver) {
        root.style.display= DisplayStyle.Flex;
        currentInteractor = interactor;
        //allQuests.Clear();
        allQuests = quests;
        questGiver = giver;

        selectedQuestView.HideSelectedQuest();
        if (currentInteractor.GameObject.TryGetComponent<IQuester>(out var quester))
        {
            Debug.Log("showing quest giver view");
            questGiverView.ShowQuestGiverView(allQuests, questGiver, quester);
        }
        else {
            Debug.Log("Failed opening");
            CloseDisplay();
        }
        

    }

    public void CloseDisplay() {
        root.style.display = DisplayStyle.None;
        selectedQuestView.HideSelectedQuest();
        questGiverView.CloseQuestGiverView();

        currentInteractor = null;
       
        currentQuest = null;
    }



    public void HandleAcceptQuest() {
        Debug.Log(currentInteractor.GameObject);
        if (currentInteractor.GameObject.TryGetComponent<IQuester>(out var quester)) {
            Debug.Log($"accepted quest from questGiver, quester is {quester}, currentQuest is {currentQuest}");
            var newQuest = currentQuest.CreateQuest(currentQuest.ID);
            Debug.Log($"new quest ID: {currentQuest.ID}   newQuest: {newQuest}");
            qService.TryAcceptQuest(quester, newQuest);
        }

        BackToQuestGiver();
        
    }

}
