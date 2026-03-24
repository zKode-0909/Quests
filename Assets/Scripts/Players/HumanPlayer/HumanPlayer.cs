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

    ISelectable currentSelection;






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

    protected override void Start()
    {
        base.Start();
        EventBus<PlayerLoadedEvent>.Raise(new PlayerLoadedEvent(new PlayerDTO(entityRuntimeID, level, "testPlayerName", health.GetMaxHealth()
            , health.GetCurrentHealth(), this)));
        questScanTimer.Start();
        
    }

    protected override void Update()
    {
        base.Update();
        questScanTimer.Tick(Time.deltaTime);

        
    }

    public void HandleSelect(Vector2 mousePos)
    {
        /*
        if (selectionManager.TryGetSelection(mousePos, out var hit))
        {
            if (currentSelection != null &&
                hit.EntityRuntimeID == currentSelection.EntityRuntimeID)
            {
                Debug.Log("Clicked same target, keeping current selection");
                return;
            }

            currentSelection = hit;
            EventBus<SelectionChangedEvent>.Raise(new SelectionChangedEvent(hit, mousePos));
        }
        else
        {
            currentSelection = null;
            EventBus<SelectionChangedEvent>.Raise(new SelectionChangedEvent(null, mousePos));
        }*/
    }

    public override SelectableData SendSelectionData()
    {
        return new SelectableData(health.GetMaxHealth(), health.GetCurrentHealth(), false, true, true, true, $"humanyname {UnityEngine.Random.Range(0, 1000)}");
    }

    private void HandleRescan() {
        playerQuests.ForceRescanNearby();
        
    }

    private void ResetScanTimer() { 
        questScanTimer.Reset();
        questScanTimer.Start();
    }

    

    
}
