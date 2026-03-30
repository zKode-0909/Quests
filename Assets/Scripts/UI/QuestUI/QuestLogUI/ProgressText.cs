
using UnityEngine;
using UnityEngine.UIElements;

public class ProgressText : VisualElement
{
    public string id;
    public Label progressText;

    public ProgressText(string id) {
        this.id = id;
        
        progressText = new Label();

        Add(progressText);
    }

    public void UpdateText(string newText) { 
        progressText.text = newText;
    }
}
