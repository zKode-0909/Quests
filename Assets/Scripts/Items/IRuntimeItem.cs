using UnityEngine;

public interface IRuntimeItem
{
    string StableID { get;}
    string ItemName { get;}
    int maxStack { get;}
    Sprite Icon { get;}
    float weight { get;}

    int runtimeID { get; }

    public void HandleLeftClick();
    public void HandleRightClick();

   
}
