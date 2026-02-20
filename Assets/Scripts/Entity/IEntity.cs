using UnityEngine;

public interface IEntity
{
    GameObject GameObject { get; }
    int EntityRuntimeID { get; }

    int EntityLevel { get; }

   
}
