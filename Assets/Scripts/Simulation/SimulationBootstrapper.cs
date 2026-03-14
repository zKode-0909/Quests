using UnityEngine;

public class SimulationBootstrapper : MonoBehaviour
{
    [SerializeField] PlayerTemplateDB dB;
    SimulationManager simManager;
    public void Bootstrap() { 
        simManager = new SimulationManager();
        simManager.Initialize(dB);
    }
}
