using UnityEngine;

public readonly struct GatherItemEvent : IEvent
{
    public readonly string gatheredByStableID;
    public readonly string itemStableID;

    public GatherItemEvent(string gathererID, string itemID)
    {
        this.gatheredByStableID = gathererID;
        this.itemStableID = itemID;
    }
}
