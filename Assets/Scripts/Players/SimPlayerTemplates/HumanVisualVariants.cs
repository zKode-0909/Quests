
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HumanVisualVariants", menuName = "SimPlayerVariants/HumanVisualVariants")]
public class HumanVisualVariants : BaseVisualVariants
{
    public List<Material> colors;

    public override void ApplyVariant(Player player)
    {
        if (colors == null || colors.Count == 0)
        {
            Debug.LogWarning("HumanVisualVariants has no colors defined.");
            return;
        }

        int index = Random.Range(0, colors.Count);
        var material = colors[index];

        var renderer = player.GetComponent<Renderer>();
        renderer.sharedMaterial = material;
    }
}
