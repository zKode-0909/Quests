using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerRegistry
{
    



    private readonly Dictionary<string, Player> players = new();

    public Dictionary<string,Player> Players => players;

    public bool TryGetPlayer(string id, out Player player) => players.TryGetValue(id, out player);

    public bool TryRegisterPlayer(Player player, string id) => players.TryAdd(id, player);

    public bool Remove(string id) => players.Remove(id);

    
}
