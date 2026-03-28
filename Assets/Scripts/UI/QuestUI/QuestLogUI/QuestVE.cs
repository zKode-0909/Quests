
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestVE : Button
{
    string QuestStableID;
    string QuestName;
    Label QuestLabel;
    public event Action<string> QuestClicked;

    public QuestVE(string name, string questStableID,string status)
    {
        QuestName = name;
        QuestLabel = new Label();
        QuestLabel.AddToClassList("questLabel");
        this.Add(QuestLabel);

        this.AddToClassList("quest");
        QuestStableID = questStableID;

        if (status == "Complete")
        {
            QuestLabel.text = ($"{name} (Complete)");
        }
        else {
            QuestLabel.text = name;
        }


        clicked += () => OnQuestClicked(); 
        
    }

    void OnQuestClicked()
    {
        //QuestClicked?.Invoke(QuestStableID);
        Debug.Log($"just cliked {QuestName}");
    }

}
