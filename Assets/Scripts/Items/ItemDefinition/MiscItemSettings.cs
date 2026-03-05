using UnityEngine;

[CreateAssetMenu(fileName = "MiscItemSettings", menuName = "Items/MiscItemSettings")]
public sealed class MiscItemSettings : ItemSettings {

    public override IRuntimeItem CreateItem()
    {
        return new MiscItem(this);
    }

}