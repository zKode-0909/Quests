using System.Collections.Generic;
using UnityEngine;

public class CharacterRegistry
{
    private readonly Dictionary<int, ICharacter> registryByID = new();

    public void Register(ICharacter c) => registryByID[c.EntityRuntimeID] = c;
    public void Unregister(ICharacter c)
    {
        if (registryByID.TryGetValue(c.EntityRuntimeID, out var cur) && ReferenceEquals(cur, c))
            registryByID.Remove(c.EntityRuntimeID);
    }

    public bool TryGet(int id, out ICharacter c) => registryByID.TryGetValue(id, out c);

    public ICharacter GetRequired(int id) =>
        registryByID.TryGetValue(id, out var c) ? c : throw new KeyNotFoundException($"No character {id}");
}
