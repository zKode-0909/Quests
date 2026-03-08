using UnityEngine;

public readonly struct InventoryUIItem
{
    public readonly string title;
    public readonly Sprite sprite;


    public InventoryUIItem(string title,Sprite sprite)
    {
        this.title = title;
        this.sprite = sprite;
    }

}
