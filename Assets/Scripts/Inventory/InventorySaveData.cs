using UnityEngine;

[System.Serializable]
public class InventorySaveData
{
    [SerializeField] string ownerStableID;
    [SerializeField] string[] items;
    [SerializeField] int capacity;

    public string Owner => ownerStableID;
    public int Capacity => capacity;
    public string[] Items => items;    

    public InventorySaveData(string owner,IRuntimeItem[] runtimeItems,int cap) {
        this.ownerStableID = owner;
        this.capacity = cap;

        items = new string[capacity];

        for (int i = 0; i < capacity; i++)
        {
            if (runtimeItems[i] != null)
            {
                items[i] = runtimeItems[i].StableID;
            }
            else { 
                runtimeItems[i] = null;
            }
        }

    }


}
