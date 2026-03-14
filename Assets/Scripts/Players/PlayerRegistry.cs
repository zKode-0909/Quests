using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerRegistry
{
    



    private readonly Dictionary<int, Player> players = new();

    public bool TryGetPlayer(int id, out Player player) => players.TryGetValue(id, out player);

    public bool TryRegisterPlayer(Player player, int id) => players.TryAdd(id, player);

    public bool Remove(int id) => players.Remove(id);
}
