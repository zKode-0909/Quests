
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDB", menuName = "Databases/ItemDB")]
public class ItemDB : ScriptableObject
{
    [SerializeField] List<ItemSettings> Items = new();

    Dictionary<string, ItemSettings> ItemsByID;

    public void BuildItemDB()
    {

        ItemsByID = new Dictionary<string, ItemSettings>(Items.Count);

        foreach (var Item in Items)
        {
            if (Item == null || string.IsNullOrWhiteSpace(Item.StableID)) continue;

            if (ItemsByID.ContainsKey(Item.StableID))
                Debug.LogError($"Duplicate ItemId: {Item.StableID}");
            else
                ItemsByID.Add(Item.StableID, Item);
        }
    }

    public bool TryGetItemDef(string itemID, out ItemSettings item)
    {
        if (ItemsByID == null)
        {
            BuildItemDB();
        }
        if (ItemsByID.TryGetValue(itemID, out var i))
        {
            item = i;
            return true;
        }
        else
        {
            item = null;
            return false;
        }
    }
}
