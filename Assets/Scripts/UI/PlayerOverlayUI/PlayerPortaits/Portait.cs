using System;
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
    public bool partyPortrait { get;private set; }

    VisualElement portrait;
    ResourceBar healthBar;

    ISelectable displayed;

    public event Action<Vector2,ISelectable> showContextMenuOnPortrait;

    public Portrait(ISelectable entityToDisplay, bool partyPortrait)
    {

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
        this.partyPortrait = partyPortrait;

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
        this.pickingMode = PickingMode.Position;
        AddToClassList("portraitRoot");

        portrait = new VisualElement();
        portrait.AddToClassList("portraitHolder");
        portrait.pickingMode = PickingMode.Position;

        var portraitNameText = new Label($"{portraitName}");
        portraitNameText.AddToClassList("portraitNameText");
        portrait.Add(portraitNameText);

        healthBar = new ResourceBar(Color.green);
        healthBar.pickingMode = PickingMode.Ignore;

        Add(portrait);
        Add(healthBar);

        portrait.RegisterCallback<MouseDownEvent>(OnPortraitClicked);



    }

    void OnPortraitClicked(MouseDownEvent evt)
    {
        Debug.Log($"MouseDown fired on portrait {id}, button = {evt.button}");

        if (evt.button == 1)
        {
            evt.StopPropagation();

            var displayedData = displayed.SendSelectionData();
            Debug.Log($"Right clicked portrait for {displayed.EntityRuntimeID}");
            showContextMenuOnPortrait?.Invoke(evt.mousePosition,displayed);
        }
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
