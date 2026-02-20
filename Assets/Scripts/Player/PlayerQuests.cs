using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerQuests : MonoBehaviour
{

    [SerializeField] public Player player;


    [SerializeField] float questGiverDetectionRadius = 500f;
    [SerializeField] LayerMask layerMask;

    
    private bool isOpen;
    public void ToggleQuestLog()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            Debug.Log($"Displaying Quest Log");
            EventBus<RequestDisplayQuestLogEvent>.Raise(new RequestDisplayQuestLogEvent(player.EntityRuntimeID));
        }

        else {
            EventBus<RequestCloseQuestLogEvent>.Raise(new RequestCloseQuestLogEvent());
        }
    }

    


    public void ForceRescanNearby()
    {
        Debug.Log("Doing scans");
        var hits = Physics.OverlapSphere(
            this.gameObject.transform.position,
            questGiverDetectionRadius,
            layerMask
        );

        foreach (var col in hits)
        {
            var giver = col.GetComponentInParent<QuestGiver>();
            if (giver != null) {
                Debug.Log("Found QuestGiver");
                EventBus<RequestQuestGiverIconDisplay>.Raise(new RequestQuestGiverIconDisplay(giver.EntityRuntimeID, player.EntityRuntimeID,player.playerLevelling.playerLevel));  
            }
                
        }
    }






}


