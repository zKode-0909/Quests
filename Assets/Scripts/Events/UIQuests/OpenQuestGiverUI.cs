using System.Collections.Generic;
using UnityEngine;

public struct OpenQuestGiverUI : IEvent
{
    public readonly IReadOnlyList<QuestUIItem> questsToShow;
    public string questGiverName;
    public string questerName;
    public string questGiverID;
    public string questerID;
    public int questerLevel;

    public OpenQuestGiverUI(IReadOnlyList<QuestUIItem> questsToShow,string giverName,string questerName,string questGiverID,string questerID,int level) {
        this.questsToShow = questsToShow;
        this.questGiverName = giverName;
        this.questerName = questerName;
        this.questGiverID = questGiverID;
        this.questerID = questerID;
        this.questerLevel = level;
    }
}
