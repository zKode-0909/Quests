using UnityEngine;

public class PartyBootstrapper : MonoBehaviour
{
    PartyController controller;
    PartyFactory factory;
    ActivePartyRegistry activePartyRegistry;
    public void Bootstrap() { 
        factory = new PartyFactory();
        activePartyRegistry = new ActivePartyRegistry();

        controller = new PartyController(factory, activePartyRegistry);
    }
}
