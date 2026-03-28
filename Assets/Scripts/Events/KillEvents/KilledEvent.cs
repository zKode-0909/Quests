using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public readonly struct KilledEvent : IEvent
{
    public readonly string killedByStableID;
    public readonly string killedCreatureStableID;

    public KilledEvent(string killerID,string creatureID) { 
        this.killedByStableID = killerID;
        this.killedCreatureStableID = creatureID;
    }
}
