using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class HumanPlayerQuests
{

    HumanPlayer player;


    float questGiverDetectionRadius = 500f;
    LayerMask layerMask;

    
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

    public void Initialize(HumanPlayer player,LayerMask layerMask) { 
        this.player = player;
        this.layerMask = layerMask;
    }


    public void ForceRescanNearby()
    {
        Debug.Log("Scanning for questgivers");
        var hits = Physics.OverlapSphere(
            player.gameObject.transform.position,
            questGiverDetectionRadius,
            layerMask
        );

        foreach (var col in hits)
        {
            var giver = col.GetComponentInParent<QuestGiver>();
            if (giver != null) {
                Debug.Log($"found questgiver, my current level is {player.EntityLevel}");
                EventBus<RequestQuestGiverIconDisplay>.Raise(new RequestQuestGiverIconDisplay(giver.EntityRuntimeID, player.EntityRuntimeID,player.EntityLevel));  
            }
                
        }
    }






}


