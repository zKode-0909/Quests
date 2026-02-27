using System.Collections.Generic;
using UnityEngine;

public class PlayerRegistry
{
    private readonly Dictionary<int, IPlayer> registryByID = new();

    public void Register(IPlayer p) => registryByID[p.EntityRuntimeID] = p;
    public void Unregister(IPlayer p)
    {
        if (registryByID.TryGetValue(p.EntityRuntimeID, out var cur) && ReferenceEquals(cur, p))
            registryByID.Remove(p.EntityRuntimeID);
    }

    public bool TryGet(int id, out IPlayer p) => registryByID.TryGetValue(id, out p);

    public IPlayer GetRequired(int id) =>
        registryByID.TryGetValue(id, out var p) ? p : throw new KeyNotFoundException($"No player {id}");
}
