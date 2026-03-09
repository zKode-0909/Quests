using UnityEngine;

public struct PlayerLoadedEvent : IEvent
{
    public PlayerDTO playerStats;

    public PlayerLoadedEvent(PlayerDTO stats)
    {
        this.playerStats = stats;
    }

}