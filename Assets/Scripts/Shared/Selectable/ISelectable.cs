using JetBrains.Annotations;
using System;
using UnityEngine;

public interface ISelectable
{
    public event Action<int> healthChangedEvent;
    int EntityRuntimeID { get; }
    void SendSelectionData(SelectableData data);

}
