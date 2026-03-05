using UnityEngine;

[CreateAssetMenu(fileName = "ItemSettings", menuName = "Items/BaseItemSettings")]
public abstract class ItemSettings : ScriptableObject
{
    [SerializeField] public string StableID;
    [SerializeField] public int maxStackSize;
    [SerializeField] public Sprite Icon;
    [SerializeField] public float weight;
    [SerializeField] public string itemName;

    public abstract IRuntimeItem CreateItem();

}



