using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestStageDetailsSettings", menuName = "Scriptable Objects/QuestStageDetailsSettings")]
public class QuestStageDetailsSettings : ScriptableObject
{
    [SerializeField] List<ObjectiveRequirementSettings> Prerequisites;
    [SerializeField] List<ObjectiveRequirementSettings> StageRequirements;
    [SerializeField] List<QuestStageDetailsSettings> PossibleNextStages;

    [SerializeField]
    private List<ConditionGroup> Conditions;

    [SerializeField] List<QuestAction> IncrementRequirementActions;
    [SerializeField] List<QuestAction> FinishedRequirementActions;
    [SerializeField] List<QuestAction> StageEndActions;
    [SerializeField] List<QuestAction> StageStartActions;


    public QuestStageDetails BuildRuntimeStageDetails(QuestBuildContext ctx)
    {
        // already built? return same runtime instance
        if (ctx.StageCache.TryGetValue(this, out var existing))
            return existing;

        // cycle detection (designer mistake)
        if (!ctx.Visiting.Add(this))
            throw new System.Exception($"Cycle detected in quest stage graph at: {name}");

        // create the runtime node FIRST and cache it
        var runtime = new QuestStageDetails(
            BuildReqs(Prerequisites),
            BuildReqs(StageRequirements),
            BuildConditionGroups(Conditions),
            IncrementRequirementActions,
            FinishedRequirementActions,
            StageEndActions,
            StageStartActions
        );

        ctx.StageCache[this] = runtime;

        // now link next stages
        var next = new List<QuestStageDetails>(PossibleNextStages?.Count ?? 0);
        if (PossibleNextStages != null)
        {
            foreach (var nextSettings in PossibleNextStages)
                next.Add(nextSettings.BuildRuntimeStageDetails(ctx));
        }
        runtime.SetNextStages(next); // or pass in ctor, whichever you prefer

        ctx.Visiting.Remove(this);
        return runtime;
    }

    private static IReadOnlyList<ObjectiveStageRequirement> BuildReqs(IReadOnlyList<ObjectiveRequirementSettings> settings)
    {
        if (settings == null) return System.Array.Empty<ObjectiveStageRequirement>();
        var list = new List<ObjectiveStageRequirement>(settings.Count);
        foreach (var s in settings)
            list.Add(s.BuildRuntimeRequirement());
        return list;
    }

    private static IReadOnlyList<IReadOnlyList<ObjectiveStageRequirement>> BuildConditionGroups(
    List<ConditionGroup> conditionSettings
)
    {
        if (conditionSettings == null || conditionSettings.Count == 0) {
            Debug.Log("emptyConditionSettings");
            return System.Array.Empty<IReadOnlyList<ObjectiveStageRequirement>>();
        }
            

        var groups = new List<IReadOnlyList<ObjectiveStageRequirement>>(conditionSettings.Count);

        foreach (var group in conditionSettings)
            groups.Add(BuildReqs(group.Requirements)); // BuildReqs already takes List<ObjectiveRequirementSettings>


        return groups;
    }
}
