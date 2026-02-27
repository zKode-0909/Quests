
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestObjectiveSettings", menuName = "QuestSettings/QuestObjectiveSettings")]
public class QuestObjectiveSettings : ScriptableObject
{
    public List<QuestStageDetails> questObjectives;


    /*
    public QuestObjective CreateRuntimeQuestObjective() {
        return new QuestObjective(questObjectives);
    }*/

    public QuestObjective CreateRuntimeQuestObjective()
    {
        // Deep clone: new list, new stages, new objectives, new requirements
        var runtimeStages = new List<QuestStageDetails>(questObjectives.Count);

        for (int i = 0; i < questObjectives.Count; i++)
        {
            var defStage = questObjectives[i];

            var runtimeReqs = new List<QuestObjectiveDetails>(defStage.requirements.Count);

            for (int j = 0; j < defStage.requirements.Count; j++)
            {
                var defObj = defStage.requirements[j];
                var defReq = defObj.requirement;

                var runtimeReq = new ObjectiveStageRequirement(
                    defReq.GetMaxProgressCount(),
                    0,
                    defReq.GetQuestObjectiveStableID()
                );

                runtimeReqs.Add(new QuestObjectiveDetails
                {
                    requirement = runtimeReq,
                    action = defObj.action
                });
            }

            runtimeStages.Add(new QuestStageDetails
            {
                requirements = runtimeReqs
            });
        }

        return new QuestObjective(runtimeStages);
    }


}
