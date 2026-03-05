using System.Collections.Generic;
using UnityEngine;

public class CharacterRegistry
{
    private readonly Dictionary<string, ICharacter> registryByStableID = new();

    public void Register(ICharacter c) => registryByStableID[c.StableID] = c;
    public void Unregister(ICharacter c)
    {
        if (registryByStableID.TryGetValue(c.StableID, out var cur) && ReferenceEquals(cur, c))
            registryByStableID.Remove(c.StableID);
    }

    public bool TryGet(string id, out ICharacter c) => registryByStableID.TryGetValue(id, out c);

    public ICharacter GetRequired(string id) =>
        registryByStableID.TryGetValue(id, out var c) ? c : throw new KeyNotFoundException($"No character {id}");
}
