using UnityEngine;

public class Quest
{
    public string questName;
    public string questGiverID;
    public string questID;  

    public Quest(string name,string questGiverID,string questID)
    {
        this.questName = name;
        this.questGiverID = questGiverID;
        this.questID = questID;
    }   
}
