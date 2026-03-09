using System.Collections.Generic;
using UnityEngine;

public class Party
{
    List<int> PartyMembers;

    int capacity = 5;
    int runtimeId;
    int leaderId;

    public Party(int leader,int id) { 
        leaderId = leader;
        runtimeId = id;
    }

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

    public int GetPartyLeader() { 
        return leaderId;
    }

    public int GetPartyId() { 
        return runtimeId;
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

    public bool CheckForPlayer(int entityId) {
        foreach (var member in PartyMembers) {
            if (member == entityId) { 
                return true;
            }
        }

        return false;
    }
}
