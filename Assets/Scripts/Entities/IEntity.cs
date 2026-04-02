using UnityEngine;

public interface IEntity
{
    string StableID { get; }
    int EntityRuntimeID { get; }

    EntityHealth Health { get; }
    int EntityLevel { get; }

   
}
