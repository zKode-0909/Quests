using System;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;


public class HumanPlayer : Player
{
    
   
    public HumanPlayerQuests playerQuests;
    //public PlayerLevelling playerLevelling;
    public HumanPlayerInventoryToggle humanInventoryToggle;
    //[SerializeField] HumanPlayerMotor humanMotor;
    public CountdownTimer questScanTimer;

    public SelectionManager selectionManager;


    
    
    public void InitializeHumanPlayer(HumanPlayerQuests quests,HumanPlayerInventoryToggle inventoryToggle) {
        this.playerQuests = quests;
        this.humanInventoryToggle = inventoryToggle;
        level = 5;
        questScanTimer = new CountdownTimer(10);
        questScanTimer.OnTimerStart += HandleRescan;
        questScanTimer.OnTimerStop += ResetScanTimer;

        
    }

    private void Awake()
    {
        
    }

    private void Start()
    {
        questScanTimer.Start();
    }

    private void Update()
    {
        questScanTimer.Tick(Time.deltaTime);
        
    }

    public void HandleSelect(Vector2 mousePos) {
        if (selectionManager.TryGetSelection(mousePos, out var selected)) {
            EventBus<SelectionChangedEvent>.Raise(new SelectionChangedEvent(selected));
        }
    }

    private void HandleRescan() {
        playerQuests.ForceRescanNearby();
        
    }

    private void ResetScanTimer() { 
        questScanTimer.Reset();
        questScanTimer.Start();
    }















}
