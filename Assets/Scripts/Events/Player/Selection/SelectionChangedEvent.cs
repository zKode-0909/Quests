using UnityEngine;

public struct SelectionChangedEvent : IEvent
{
    public ISelectable selected;
    public Vector2 mousePos;


    public SelectionChangedEvent(ISelectable selection,Vector2 pos)
    {
        selected = selection;
        mousePos = pos;
    }
}
