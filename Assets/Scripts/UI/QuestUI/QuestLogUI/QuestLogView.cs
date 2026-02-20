using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestLogView : MonoBehaviour
{
    [SerializeField] UIDocument document;
    [SerializeField] StyleSheet styleSheet;
    VisualElement root;
    VisualElement titleHolder;
    Label titleLabel;
    VisualElement bodyHolder;
    VisualElement questLogHolder;

    EventBinding<DisplayQuestLogEvent> displayQuestLog;
    EventBinding<CloseQuestLogEvent> closeQuestLog; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        displayQuestLog = new EventBinding<DisplayQuestLogEvent>(OpenQuestLog);
        EventBus<DisplayQuestLogEvent>.Register(displayQuestLog);

        closeQuestLog = new EventBinding<CloseQuestLogEvent>(CloseQuestLog);
        EventBus<CloseQuestLogEvent>.Register(closeQuestLog);

        root = document.rootVisualElement;
        root.Clear();

        root.styleSheets.Add(styleSheet);

        BuildQuestLogView();
        root.AddToClassList("questLogDisplay");
    }


    void BuildQuestLogView() {
        questLogHolder = new VisualElement();
        questLogHolder.AddToClassList("questLogHolder");

        titleHolder = new VisualElement();
        titleLabel = new Label();
        titleLabel.text = "QUEST LOG";
        titleHolder.AddToClassList("titleHolder");
        titleHolder.Add(titleLabel);

        bodyHolder = new VisualElement();
        bodyHolder.AddToClassList("questLogBody");
        questLogHolder.Add(titleHolder);
        questLogHolder.Add(bodyHolder);
        root.Add(questLogHolder);

        root.style.display = DisplayStyle.None;

    }
    
    public void OpenQuestLog(DisplayQuestLogEvent evt) {
        root.style.display = DisplayStyle.Flex;
        var quests = evt.Quests;

        foreach (var quest in quests) {
            var questVE = new QuestVE(quest.title);
            bodyHolder.Add(questVE);
        }
    }

    
    public void CloseQuestLog() { 
        bodyHolder.Clear();
        root.style.display = DisplayStyle.None;
    }
}
