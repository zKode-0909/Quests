using System.Collections.Generic;
using UnityEngine;

public struct OpenQuestGiverUI : IEvent
{
    public readonly IReadOnlyList<QuestUIItem> questsToShow;
    public string questGiverName;
    public string questerName;
    public int questGiverID;
    public int questerID;

    public OpenQuestGiverUI(IReadOnlyList<QuestUIItem> questsToShow,string giverName,string questerName,int questGiverID,int questerID) {
        this.questsToShow = questsToShow;
        this.questGiverName = giverName;
        this.questerName = questerName;
        this.questGiverID = questGiverID;
        this.questerID = questerID;
    }
}
