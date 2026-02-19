using UnityEngine;

public interface IInteractor
{
   // Transform Transform { get; }          // where interaction came from
    GameObject GameObject { get; }

    int EntityRuntimeID { get; }
}
