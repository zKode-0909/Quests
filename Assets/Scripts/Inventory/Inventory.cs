using UnityEngine;


public class Inventory
{
    int capacity;
    IRuntimeItem[] items;
    public string ownerStableID;
    public Inventory(int capacity,string ownerStableID) { 
        this.capacity = capacity;
        items = new IRuntimeItem[capacity];
        this.ownerStableID = ownerStableID;
    }

    public bool TryAddItemToInventory(IRuntimeItem item) {
        int idx = 0;
        foreach (var i in items) {
            if (i == null) {
                items[idx] = item;
                if (item != null)
                {
                    Debug.Log($"added {item.ItemName} to inventory");
                }
                else {
                    Debug.Log("Added null to inventory");
                }
                
                return true;
            }
            idx++;
        }

        return false;
    }

    public void SetItemAtIndex(int index, IRuntimeItem item)
    {
        items[index] = item;
    }

    public IRuntimeItem[] GetItems() { 
        return items;
    }

    public void SwapItems(int idx1,int idx2) {
        Debug.Log("Inventory array before swap");
        for (int i = 0; i < items.Length; i++) {
            Debug.Log($"item at idx{i}: {items[i]}");
        }
        var item1 = items[idx1];
        var item2 = items[idx2];
        items[idx1] = item2;
        items[idx2] = item1;
        Debug.Log("Inventory array after swap");
        for (int i = 0; i < items.Length; i++) {
            Debug.Log($"item at idx{i}: {items[i]}");
        }

    }
}
