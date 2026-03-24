using UnityEngine;
using UnityEngine.UIElements;

public class PortraitContextMenu : VisualElement
{
    VisualElement buttonsHolder;
    Button inviteToPartyButton;
    Button tradeButton;
    Button inspectButton;
    Button testButton;
    ISelectable currentContextOwner;
    ISelectable overlayOwner;
    
    public PortraitContextMenu(ISelectable overlayOwner) {
        this.overlayOwner = overlayOwner;
        BuildButtons();
    }


    void BuildButtons() { 
        inviteToPartyButton = new Button();
        inviteToPartyButton.text = "Invite to party";
        inviteToPartyButton.clicked += () => OnInviteClicked();

        tradeButton = new Button();
        tradeButton.text = "Trade";
        
        inspectButton = new Button();
        inspectButton.text = "Inspect";

        testButton = new Button();
        testButton.text = "Test";



        buttonsHolder = new VisualElement();
        buttonsHolder.AddToClassList("contextMenuButtonsHolder");

        Add(buttonsHolder);
    }

    public void BuildHumanPlayerContextMenu(ISelectable human) { 
        currentContextOwner = human;

        buttonsHolder.Clear();
        buttonsHolder.Add(testButton);
    }

    public void BuildSimPlayerContextMenu(ISelectable sim) { 
        currentContextOwner = sim;
        buttonsHolder.Clear();
        buttonsHolder.Add(inviteToPartyButton);
    
    }

    public void OnInviteClicked() {
        Debug.Log("invite clicked");
        var selectionData = currentContextOwner.SendSelectionData();
        var humanData = overlayOwner.SendSelectionData();
        EventBus<RequestPartyInviteEvent>.Raise(new RequestPartyInviteEvent(overlayOwner.EntityRuntimeID, currentContextOwner));
        //Debug.Log($"{humanData.selectedName} trying to invite {selectionData.selectedName} to party;p");
    }
}
