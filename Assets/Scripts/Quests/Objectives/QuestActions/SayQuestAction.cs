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

        NPCActionRunner.NPCSayAction(NPCStableID, wordsToSay);
       // sayAction.Invoke();
    }
}
