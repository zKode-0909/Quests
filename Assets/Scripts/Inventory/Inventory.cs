using UnityEngine;


public class Inventory
{
    int capacity;
    IRuntimeItem[] items;
    public Inventory(int capacity) { 
        this.capacity = capacity;
        items = new IRuntimeItem[capacity];
    }

    public bool TryAddItemToInventory(IRuntimeItem item) {
        int idx = 0;
        foreach (var i in items) {
            if (i == null) {
                items[idx] = item;
                return true;
            }
            idx++;
        }

        return false;
    }
}
