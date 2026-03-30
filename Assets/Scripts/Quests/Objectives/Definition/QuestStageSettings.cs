using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestStageSettings", menuName = "Scriptable Objects/QuestStageSettings")]
public class QuestStageSettings : ScriptableObject
{
    [SerializeField] List<QuestAction> AllStagesCompleteActions;
    [SerializeField] QuestStageDetailsSettings InitialStage;
    

    public QuestStages BuildRuntimeQuestStages()
    {
        List<QuestStageDetails> stages = new List<QuestStageDetails>();
        var ctx = new QuestBuildContext();
        var root = InitialStage.BuildRuntimeStageDetails(ctx,stages);

        // Optionally include all stages, not just root:
        // ctx.StageCache.Values has every stage built.
        return new QuestStages(root,AllStagesCompleteActions,stages);
    }
}
