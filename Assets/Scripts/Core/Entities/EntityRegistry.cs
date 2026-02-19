using System.Collections.Generic;
using UnityEngine;

public static class EntityRegistry
{
    private static readonly Dictionary<int, IEntity> registryByID = new();

    public static void Register(IEntity e) => registryByID[e.EntityRuntimeID] = e;
    public static void Unregister(IEntity e)
    {
        if (registryByID.TryGetValue(e.EntityRuntimeID, out var cur) && ReferenceEquals(cur, e))
            registryByID.Remove(e.EntityRuntimeID);
    }

    public static bool TryGet(int id, out IEntity e) => registryByID.TryGetValue(id, out e);

    public static IEntity GetRequired(int id) =>
        registryByID.TryGetValue(id, out var e) ? e : throw new KeyNotFoundException($"No entity {id}");
}
