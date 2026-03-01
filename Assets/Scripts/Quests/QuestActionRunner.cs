using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestActionRunner
{
    public void HandleActions(IReadOnlyList<QuestAction> actions) {
        Debug.Log("running action");
    }
}
