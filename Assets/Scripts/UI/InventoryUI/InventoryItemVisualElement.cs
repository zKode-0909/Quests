using System;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryItemVisualElement : VisualElement
{
    
    //VisualElement Icon;
    Sprite IconSprite;

    public InventoryItemVisualElement(string title,Sprite icon) {
        //Icon = new VisualElement();
        this.AddToClassList("inventoryIcon");

       
       

        this.style.backgroundImage = new StyleBackground(icon);
        IconSprite = icon;
        this.pickingMode = PickingMode.Ignore;
        //this.Add(Icon);

        

    }

    public void SetBackgroundImage(Sprite icon) {
        IconSprite = icon;
        this.style.backgroundImage = new StyleBackground(icon);
        Debug.Log($"set background image to: {icon}");
    }

    public Sprite GetItemSprite() {
        return IconSprite;    
    }
}
