using UnityEngine;

public class PartyFactory
{
    public Party CreateParty(int leaderId) {
        return new Party(leaderId,RuntimeIDGenerator.GetNext());
    }
}
