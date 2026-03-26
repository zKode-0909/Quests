using System.Collections.Generic;
using UnityEngine;

public class ActivePartyRegistry
{

    private readonly Dictionary<int, Party> partyByMember = new();
    private readonly Dictionary<int, Party> partyById = new();

    public bool TryGetActivePartyByMember(int entityId, out Party party)
    {
        return partyByMember.TryGetValue(entityId, out party);
    }

    public bool TryGetPartyById(int partyId, out Party party)
    {
        return partyById.TryGetValue(partyId, out party);
    }

    public bool TryRegisterParty(Party partyToRegister)
    {
        int leaderId = partyToRegister.GetPartyLeader();

        int partyId = partyToRegister.GetPartyId();
        if (partyByMember.ContainsKey(leaderId))
            return false;

        if (partyById.ContainsKey(partyId))
            return false;

        partyById.Add(partyId, partyToRegister);
        partyByMember.Add(leaderId, partyToRegister);
        Debug.Log($"{leaderId} has successfully started a party with id {partyId}");
        return true;
    }

    public bool TryRegisterMemberToParty(int memberId, Party party)
    {
        if (partyByMember.ContainsKey(memberId))
            return false;

        partyByMember.Add(memberId, party);


        return true;
    }

    public bool TryUnregisterMember(int memberId)
    {

       return partyByMember.Remove(memberId);
        

        
    }


}
