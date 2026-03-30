using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestLogSaveData
{
    [SerializeField] string owner;
    [SerializeField] List<QuestSaveData> currentQuests;
    [SerializeField] List<string> completedQuests;
    [SerializeField] int capacity;

    public string Owner => owner;
    public List<QuestSaveData> CurrentQuests => currentQuests;
    public List <string> CompletedQuests => completedQuests;
    public int Capacity => capacity;

    public QuestLogSaveData(string owner,List<QuestSaveData> current,List<string> completed,int cap){
        this.owner = owner;
        this.currentQuests = current;
        this.completedQuests = completed;
        this.capacity = cap;
    }

}
