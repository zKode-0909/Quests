using System.Collections.Generic;
using UnityEngine;

public class PortraitManager
{
    List<Portrait> portraits;

    public PortraitManager() {
        portraits = new List<Portrait>();
    }

    public bool TryAddPortrait(ISelectable selectable,out Portrait newPortrait) {
        foreach (var portrait in portraits) {
            if (selectable.EntityRuntimeID == portrait.id) {
                newPortrait = null;
                return false;
            }
        }

        newPortrait = new Portrait(selectable);
        portraits.Add(newPortrait);
        return true;
    }

    public bool TryRemovePortrait(ISelectable selectable, out Portrait oldPortrait) {

        for (int i = 0; i < portraits.Count; i++) {
            if (portraits[i].id == selectable.EntityRuntimeID) {
                oldPortrait = portraits[i];
                oldPortrait.Dispose();
                portraits.Remove(portraits[i]);
                return true;
            }
        }

        oldPortrait = null;
        return false;
    }
}
