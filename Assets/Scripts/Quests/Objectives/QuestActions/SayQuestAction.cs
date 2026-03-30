using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SayQuestAction", menuName = "QuestActions/SayQuestAction")]
public class SayQuestAction : QuestAction
{
    [SerializeField] string NPCStableID;
    [SerializeField] string wordsToSay;
    [SerializeField] int actionIndex;
    public override void Execute()
    {
        Debug.Log($"In sayquestAction about to execute for {NPCStableID} with {wordsToSay}");
        NPCActionRunner.NPCSayAction(NPCStableID, wordsToSay);
   
    }
}
