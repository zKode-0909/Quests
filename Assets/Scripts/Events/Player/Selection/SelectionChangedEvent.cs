using UnityEngine;

public struct SelectionChangedEvent : IEvent
{
    public ISelectable selected;


    public SelectionChangedEvent(ISelectable selection)
    {
        selected = selection;
    }
}
