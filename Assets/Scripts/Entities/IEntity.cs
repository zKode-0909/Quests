using UnityEngine;

public interface IEntity
{
    GameObject GameObject { get; }
    int EntityRuntimeID { get; }

    EntityHealth Health { get; }
    int EntityLevel { get; }

   
}
