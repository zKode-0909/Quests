
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestVE : Button
{
    string QuestStableID;
    string QuestName;
    Label QuestLabel;
    public event Action<QuestUIItem> QuestClicked;


    QuestUIItem currentQuest;


    public QuestVE(QuestUIItem quest)
    {
        currentQuest = quest;
        QuestName = quest.title;
        QuestLabel = new Label();
        QuestLabel.AddToClassList("questLabel");
        this.Add(QuestLabel);

        this.AddToClassList("quest");
        QuestStableID = quest.questID;

        if (quest.status == "Complete")
        {
            QuestLabel.text = ($"{QuestName} (Complete)");
        }
        else {
            QuestLabel.text = QuestName;
        }


        clicked += () => OnQuestClicked(); 
        
    }

    void OnQuestClicked()
    {
        //QuestClicked?.Invoke(QuestStableID);
        QuestClicked?.Invoke(currentQuest);
        Debug.Log($"just cliked {QuestName}");
    }

}
