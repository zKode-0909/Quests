
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestVE : Button
{
    string QuestStableID;
    string QuestName;
    Label QuestLabel;
    public event Action<string> QuestClicked;
    public QuestVE(string name, string questStableID)
    {
        QuestName = name;
        QuestLabel = new Label(QuestName);
        QuestLabel.AddToClassList("questLabel");
        this.Add(QuestLabel);

        this.AddToClassList("quest");
        QuestStableID = questStableID;


        clicked += () => QuestClicked?.Invoke(QuestStableID);
        
    }

    void OnQuestClicked()
    {
        Debug.Log($"just cliked {QuestName}");
    }

}
