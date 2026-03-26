using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance;
using UnityEngine.UIElements;

public class PortraitManager
{
    List<Portrait> portraits;

    public PortraitManager() {
        portraits = new List<Portrait>();
    }

    public bool TryAddPortrait(ISelectable selectable,out Portrait newPortrait) {
        /*
        foreach (var portrait in portraits) {
            if (selectable.EntityRuntimeID == portrait.id) {
                newPortrait = null;
                return false;
            }
        }*/

        newPortrait = new Portrait(selectable,false);
        portraits.Add(newPortrait);
        return true;
    }

    public bool TryAddPartyPortrait(ISelectable selectable, VisualElement partyPortraits, out Portrait newPortrait) {

        /*
        foreach (var holder in partyPortraits.Children())
        {
            if (holder.childCount > 0 && holder[0] is Portrait existingPortrait)
            {
                if (existingPortrait.id == selectable.EntityRuntimeID)
                {
                    newPortrait = null;
                    return false;
                }
            }
        }*/

        foreach (var holder in partyPortraits.Children())
        {
            if (holder.childCount == 0)
            {
                newPortrait = new Portrait(selectable,true);
                holder.Add(newPortrait);
                portraits.Add(newPortrait);
                holder.style.display = DisplayStyle.Flex;
                return true;
            }
        }

        newPortrait = null;
        return false;

    }

    public bool TryRemovePortrait(ISelectable selectable, out Portrait oldPortrait) {

        for (int i = 0; i < portraits.Count; i++) {
            if (portraits[i].id == selectable.EntityRuntimeID && portraits[i].partyPortrait == false) {
                Debug.Log("REMOVING PORTRAIT");
                oldPortrait = portraits[i];
                oldPortrait.Dispose();
                portraits.Remove(portraits[i]);
                return true;
            }
        }

        oldPortrait = null;
        return false;
    }

    public bool TryRemovePartyPortrait(ISelectable selectable, VisualElement partyPortraits, out Portrait removedPortrait)
    {
        foreach (var holder in partyPortraits.Children())
        {
            if (holder.childCount == 0)
                continue;

            if (holder[0] is Portrait portrait && portrait.id == selectable.EntityRuntimeID)
            {
                portrait.Dispose();
                holder.Remove(portrait);
                portraits.Remove(portrait); 
                removedPortrait = portrait;
                holder.style.display = DisplayStyle.None;
                return true;
            }
        }

        removedPortrait = null;
        return false;
    }
}
