
using UnityEngine;
using UnityEngine.UIElements;

public class QuestVE : VisualElement
{
    string QuestName;
    Label QuestLabel;
    public QuestVE(string name) { 
        QuestName = name;
        QuestLabel = new Label(QuestName);
        QuestLabel.AddToClassList("questLabel");
        this.Add(QuestLabel);

        this.AddToClassList("quest");
    }
}
