using JetBrains.Annotations;
using System;
using UnityEngine;

public interface ISelectable
{
    public event Action<int> healthChangedEvent;
    int EntityRuntimeID { get; }
    string StableID { get; }

    SelectableType SelectableType { get; }
    SelectableData SendSelectionData();

    public void UpdatePartyStatus(bool status);

}
