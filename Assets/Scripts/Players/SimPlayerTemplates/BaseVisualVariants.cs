using UnityEngine;

[CreateAssetMenu(fileName = "BaseVisualVariants", menuName = "SimPlayerVariants/BaseVisualVariants")]
public abstract class BaseVisualVariants : ScriptableObject
{
    public abstract void ApplyVariant(Player player);
}
