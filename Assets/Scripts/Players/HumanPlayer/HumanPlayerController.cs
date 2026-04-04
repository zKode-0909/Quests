using log4net.Core;
using UnityEngine;

public class HumanPlayerController : IController
{
    PlayerInputReader input;
    Player player;

    HumanPlayerView view;
    HumanPlayerQuests playerQuests;
    HumanPlayerInventoryToggle inventoryToggle;
    HumanMenuToggle menuToggle;

    Rigidbody rb;

    public IMotor Motor => motor;

    IMotor motor;


    EventBinding<EntityWorldQuestStateChangedEvent> worldQuestStateChangeBinding;

    public string StableID => player.StableID;



    public void Initialize(Rigidbody rb,PlayerInputReader input,Player player,HumanPlayerView view,HumanPlayerMotor motor)
    {
        this.motor = motor;
        this.view = view;
        this.rb = view.Rb;
        this.input = input;
        this.player = player;
        rb.freezeRotation = true;
        input.MoveEvent += HandleMove;
        input.AttackEvent += HandleAttack;

        playerQuests = new HumanPlayerQuests();
        inventoryToggle = new HumanPlayerInventoryToggle();
        menuToggle = new HumanMenuToggle();

        //

        input.OpenQuestLogEvent += HandleOpenQuestLog;
        //input.SelectionEvent += HandleSelection;

        input.OpenMenuEvent += HandleShowMenu;


        input.OpenInventoryEvent += HandleOpenInventory;

        worldQuestStateChangeBinding = new EventBinding<EntityWorldQuestStateChangedEvent>(HandlePlayerQuestAccept);
        EventBus<EntityWorldQuestStateChangedEvent>.Register(worldQuestStateChangeBinding);
    }

    public void Dispose() {
        input.MoveEvent -= HandleMove;
        input.AttackEvent -= HandleAttack;
        input.OpenQuestLogEvent -= HandleOpenQuestLog;
        input.OpenInventoryEvent -= HandleOpenInventory;
        input.OpenMenuEvent -= HandleShowMenu;

        EventBus<EntityWorldQuestStateChangedEvent>.Deregister(worldQuestStateChangeBinding);
    }

    /*
    private void OnDisable()
    {
        input.MoveEvent -= HandleMove;
        input.AttackEvent -= HandleAttack;
        input.OpenQuestLogEvent -= HandleOpenQuestLog;
        input.SelectionEvent -= HandleSelection;

        input.OpenInventoryEvent -= HandleOpenInventory;
        EventBus<EntityWorldQuestStateChangedEvent>.Deregister(worldQuestStateChangeBinding);
    }*/

    
    void HandleOpenInventory() {
        Debug.Log("received open inventory input");
        inventoryToggle.ToggleInventory(player.StableID);
    }


    /*
    void HandleSelection(Vector2 pos) {
        player.HandleSelect(pos);
        
    }*/

    
    void HandleShowMenu() {
        menuToggle.ToggleMenu();
    }

    void HandleMove(Vector2 dir)
    {
        motor.SetMovementDir(dir);
    }

    void HandleAttack()
    {
        player.RequestAttack();
    }
    
    void HandleOpenQuestLog() { 
        playerQuests.ToggleQuestLog(player.StableID);
    }

    
    void HandlePlayerQuestAccept(EntityWorldQuestStateChangedEvent evt) {
        playerQuests.ForceRescanNearby();
    }

    


}