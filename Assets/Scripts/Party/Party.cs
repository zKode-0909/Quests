using System.Collections.Generic;
using UnityEngine;

public class Party
{
    List<int> PartyMembers;

    int capacity = 5;

    public bool TryAddMemberToParty(int idToAdd) {
        if (PartyMembers.Count < capacity) { 
            PartyMembers.Add(idToAdd);
            return true;
        }
        else{
            Debug.Log("party capacity reached, cant add");
            return false;
        }
    }

    public bool TryRemoveMemberFromParty(int idToRemove) {
        foreach (var member in PartyMembers) {
            if (idToRemove == member) {
                PartyMembers.Remove(member);
                return true;
            }
        }

        return false;
    }
}
