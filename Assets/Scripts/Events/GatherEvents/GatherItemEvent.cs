using UnityEngine;

public readonly struct GatherItemEvent : IEvent
{
    public readonly int gatheredByRuntimeID;
    public readonly string itemStableID;

    public GatherItemEvent(int gathererID, string itemID)
    {
        this.gatheredByRuntimeID = gathererID;
        this.itemStableID = itemID;
    }
}
