using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SayQuestAction", menuName = "Scriptable Objects/SayQuestAction")]
public class SayQuestAction : QuestAction
{
    Action<int, string> sayAction;
    public override void Execute()
    {
       // sayAction.Invoke();
    }
}
