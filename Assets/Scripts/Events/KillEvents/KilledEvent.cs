using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public readonly struct KilledEvent : IEvent
{
    public readonly int killedByRuntimeID;
    public readonly string killedCreatureStableID;

    public KilledEvent(int killerID,string creatureID) { 
        this.killedByRuntimeID = killerID;
        this.killedCreatureStableID = creatureID;
    }
}
