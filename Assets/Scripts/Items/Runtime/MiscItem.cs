using Codice.CM.Common.Serialization.Replication;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MiscItem : IRuntimeItem
{
    public string StableID => settings.StableID;

    public string ItemName => settings.StableID;

    public int maxStack => settings.maxStackSize;

    public Sprite Icon => settings.Icon;

    public float weight => settings.weight;

    public int runtimeID => ID;

    int ID;

    MiscItemSettings settings { get; set; }

    public MiscItem(MiscItemSettings settings)
    {
        this.settings = settings;
        ID = RuntimeIDGenerator.GetNext();
    }

    public void HandleLeftClick()
    {
        Debug.Log("Left Click");
    }

    public void HandleRightClick()
    {
        Debug.Log("Right click");
    }
}
