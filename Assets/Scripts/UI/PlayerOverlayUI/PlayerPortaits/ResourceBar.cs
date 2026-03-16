using UnityEngine;
using UnityEngine.UIElements;

public class ResourceBar : VisualElement
{
    
    VisualElement healthIndicator;

    public ResourceBar(Color color) { 
        
        BuildResourceBar();
        healthIndicator.style.backgroundColor = color;


    }

    void BuildResourceBar() {
        
        healthIndicator = new VisualElement();

        this.AddToClassList("healthBarHolder");
        healthIndicator.AddToClassList("healthIndicator");

        healthIndicator.style.width = Length.Percent(100);
        healthIndicator.style.height = Length.Percent(100);

        this.Add(healthIndicator);

    }

    public void SetHealthIndicator(float percent) {

        healthIndicator.style.width = Length.Percent(percent);
    
    }
}
