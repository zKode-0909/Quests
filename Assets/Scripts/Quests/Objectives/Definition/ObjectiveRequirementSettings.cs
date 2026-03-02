using UnityEngine;

[CreateAssetMenu(fileName = "ObjectiveRequirementSettings", menuName = "Scriptable Objects/ObjectiveRequirementSettings")]
public class ObjectiveRequirementSettings : ScriptableObject
{
    [SerializeField] int MaxProgress;
    [SerializeField] string TargetStableID;


    public ObjectiveStageRequirement BuildRuntimeRequirement()
    {
        return new ObjectiveStageRequirement(MaxProgress,0, TargetStableID);
    }
}
