using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class HumanPlayerQuests
{

    HumanPlayerView player;


    float questGiverDetectionRadius = 500f;
    LayerMask layerMask;

    
    private bool isOpen;
    public void ToggleQuestLog()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            Debug.Log($"Displaying Quest Log");
            //EventBus<RequestDisplayQuestLogEvent>.Raise(new RequestDisplayQuestLogEvent(player.StableID));
        }

        else {
            //EventBus<RequestCloseQuestLogEvent>.Raise(new RequestCloseQuestLogEvent());
        }
    }

    public void Initialize(HumanPlayerView player,LayerMask layerMask) { 
        this.player = player;
        this.layerMask = layerMask;
    }


    public void ForceRescanNearby()
    {
        Debug.Log("Scanning for questgivers");
        /*
        var hits = Physics.OverlapSphere(
            //this.gameObject.transform.position,
           // questGiverDetectionRadius,
            //layerMask
        );

        foreach (var col in hits)
        {
            var giver = col.GetComponentInParent<QuestGiver>();
            if (giver != null) {
                Debug.Log($"found questgiver, my current level is {player.EntityLevel}");
                EventBus<RequestQuestGiverIconDisplay>.Raise(new RequestQuestGiverIconDisplay(giver.StableID, player.StableID,player.EntityLevel));  
            }
                
        }*/
    }






}


