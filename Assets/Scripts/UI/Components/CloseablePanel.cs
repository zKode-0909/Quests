using UnityEngine;
using UnityEngine.UIElements;

public class CloseablePanel : VisualElement
{
    Label bodyText;
    Label headerText;
    Label footerText;
    Label title;

    VisualElement headerContentHolder;
    VisualElement footerContentHolder;
    VisualElement footer;

    public CloseablePanel(string header,string body,string footer,float headerPct,float footerPct, float bodyPct) { 
        
    }
}
