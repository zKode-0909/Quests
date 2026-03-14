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
    int index;

    VisualElement portrait;
    ResourceBar healthBar;

    public Portrait(SelectableData data,int idx) {
        maxHealth = data.maxHealth;
        currentHealth = data.currentHealth;
        portraitName = data.selectedName;
        elitePortrait = data.isElite;
        playerPortrait = data.isPlayer;
        friendlyPortrait = data.isFriendly;
        index = idx;

        BuildPortrait();
    }


    void BuildPortrait() {
        portrait = new VisualElement();
        

        portrait.AddToClassList("portraitHolder");


        var healthBar = new ResourceBar(Color.green);

        this.Add(portrait);
        this.Add(healthBar);
        

        
    }



    public void SetMaxHealth(int newMaxHealth) { 
        maxHealth = newMaxHealth;
    }

    public void SetCurrentHealth(int newCurrentHealth) { 
        currentHealth = newCurrentHealth;
    }

   
}
