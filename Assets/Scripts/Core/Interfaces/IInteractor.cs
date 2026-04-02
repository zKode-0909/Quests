using UnityEngine;

public interface IInteractor
{
   // Transform Transform { get; }          // where interaction came from
    string StableID { get; }
    int EntityRuntimeID { get; }
}
