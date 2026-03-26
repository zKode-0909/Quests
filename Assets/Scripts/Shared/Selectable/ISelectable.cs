using JetBrains.Annotations;
using System;
using UnityEngine;

public interface ISelectable
{
    public event Action<int> healthChangedEvent;
    int EntityRuntimeID { get; }
    SelectableData SendSelectionData();

    public void UpdatePartyStatus(bool status);

}
