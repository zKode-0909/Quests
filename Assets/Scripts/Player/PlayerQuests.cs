using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerQuests : MonoBehaviour,IQuester
{
   // [SerializeField] private QuestLogView view;
    [SerializeField] private int capacity = 25;
    [SerializeField] public Player player;
    public Dictionary<string,QuestDisplayIcon> registeredQuestGivers = new Dictionary<string,QuestDisplayIcon>();

    public QuestLog questLog;
    public QuestLog QuestLog => questLog;  // PROPERTY (matches interface)

    public HashSet<string> CompletedQuests => questLog.completedQuests;

    public int QuesterLevel => player.playerLevelling.playerLevel;

    public int EntityRuntimeID => player.EntityRuntimeID;

    [SerializeField] float questGiverDetectionRadius = 500f;
    [SerializeField] LayerMask layerMask;

    
    private bool isOpen;



    private void Awake()
    {
        questLog = new QuestLog(capacity);
    }

    public void ToggleQuestLog()
    {
        isOpen = !isOpen;

        if (isOpen) {
           
            var quests = questLog.GetQuests();
            Debug.Log("READING QUESTS.....");
            foreach (KeyValuePair<string, Quest> pair in quests) {
                Debug.Log($"Quest: {pair.Value.questName}");
            }
            Debug.Log("DONE READING QUESTS.....");
            //view.OpenQuestLog(quests);   // or questLog.Quests as IReadOnlyList
        }
            
        //else
          //  view.CloseQuestLog();
    }

    public bool TryAddQuest(Quest quest) {
        if (questLog.TryAddQuest(quest)) {
            Debug.Log("Added quest in player quests");
            ForceRescanNearby();
            return true;
        }
        return false;
    }

    public bool TryRegisterQuestGiver(QuestGiver questGiver,out QuestDisplayIcon icon) {
        icon = QuestUtils.DetermineIcon(questGiver, this);
        if (registeredQuestGivers.TryAdd(questGiver.questGiverID, icon)) { 
            return true;
        }
      
        return false;
    }


    public void ForceRescanNearby()
    {
        registeredQuestGivers.Clear();
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
                if (TryRegisterQuestGiver(giver, out var icon)) {
                    Debug.Log("Registered QuestGiver");
                    giver.SetQuestIcon(icon);
                };
                
                
            }
                
        }
    }






}


