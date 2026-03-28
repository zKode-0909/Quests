using UnityEngine;

public class HumanMenuToggle
{
    bool menuOpen = false;
    public void ToggleMenu() {
        if (menuOpen == false)
        {
            Debug.Log("Opening menu");
            EventBus<ShowMenuEvent>.Raise(new ShowMenuEvent());
            menuOpen = true;
        }
        else {
            Debug.Log("Closing menu");
            EventBus<HideMenuEvent>.Raise(new HideMenuEvent());
            menuOpen = false;
        }
    }
}
