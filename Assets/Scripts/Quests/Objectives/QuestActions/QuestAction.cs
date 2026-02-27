using UnityEngine;

[CreateAssetMenu(fileName = "QuestAction", menuName = "QuestActions/QuestAction")]
public abstract class QuestAction : ScriptableObject
{
    public abstract void Execute();
}
