using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConditionGroup
{
    [SerializeField]
    private List<ObjectiveRequirementSettings> requirements;

    public IReadOnlyList<ObjectiveRequirementSettings> Requirements => requirements;
}
