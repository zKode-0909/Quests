using UnityEngine;
using UnityEngine.UIElements;

public class Portrait : VisualElement
{
    int maxHealth;
    int currentHealth;
    string portraitName;
    bool elitePortrait;
    bool playerPortrait;
    bool friendlyPortrait;
    public int id;

    VisualElement portrait;
    ResourceBar healthBar;

    ISelectable displayed;

    public Portrait(ISelectable entityToDisplay) {

        displayed = entityToDisplay;

        displayed.healthChangedEvent += SetCurrentHealth;

        var data = displayed.SendSelectionData();
        maxHealth = data.maxHealth;
        currentHealth = data.currentHealth;
        portraitName = data.selectedName;
        elitePortrait = data.isElite;
        playerPortrait = data.isPlayer;
        friendlyPortrait = data.isFriendly;

        id = entityToDisplay.EntityRuntimeID;

        BuildPortrait();
    }

    public ISelectable GetDisplayedPortrait()
    {
        return displayed;

    }

    public void Dispose() { 
        displayed.healthChangedEvent -= SetCurrentHealth;
    }


    void BuildPortrait() {
        portrait = new VisualElement();
        

        portrait.AddToClassList("portraitHolder");


        healthBar = new ResourceBar(Color.green);

        this.Add(portrait);
        this.Add(healthBar);
        

        
    }



    public void SetMaxHealth(int newMaxHealth) { 
        maxHealth = newMaxHealth;
    }

    public void SetCurrentHealth(int newCurrentHealth) { 
        currentHealth = newCurrentHealth;
        var percent = ((float)currentHealth / (float)maxHealth) * 100;
        healthBar.SetHealthIndicator(percent);


    }

   
}
