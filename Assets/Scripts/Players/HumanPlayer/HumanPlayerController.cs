using UnityEngine;

public class HumanPlayerController : MonoBehaviour
{
    PlayerInputReader input;
    HumanPlayer player;


    Rigidbody rb;


    EventBinding<EntityWorldQuestStateChangedEvent> worldQuestStateChangeBinding;



    public void Initialize(Rigidbody rb,PlayerInputReader input,HumanPlayer player)
    {
        this.rb = rb;
        this.input = input;
        this.player = player;
        rb.freezeRotation = true;
        input.MoveEvent += HandleMove;
        input.AttackEvent += HandleAttack;
        input.OpenQuestLogEvent += HandleOpenQuestLog;
        input.SelectionEvent += HandleSelection;


        input.OpenInventoryEvent += HandleOpenInventory;

        worldQuestStateChangeBinding = new EventBinding<EntityWorldQuestStateChangedEvent>(HandlePlayerQuestAccept);
        EventBus<EntityWorldQuestStateChangedEvent>.Register(worldQuestStateChangeBinding);
    }


    private void OnDisable()
    {
        input.MoveEvent -= HandleMove;
        input.AttackEvent -= HandleAttack;
        input.OpenQuestLogEvent -= HandleOpenQuestLog;
        input.SelectionEvent -= HandleSelection;

        input.OpenInventoryEvent -= HandleOpenInventory;
        EventBus<EntityWorldQuestStateChangedEvent>.Deregister(worldQuestStateChangeBinding);
    }

    void HandleOpenInventory() {
        Debug.Log("received open inventory input");
        player.humanInventoryToggle.ToggleInventory(player.EntityRuntimeID);
    }

    void HandleSelection(Vector2 pos) {
        player.HandleSelect(pos);
        
    }

    void HandleMove(Vector2 dir)
    {
        player.playerMotor.SetMovementDir(dir);
    }

    void HandleAttack()
    {
        player.RequestAttack();
    }

    void HandleOpenQuestLog() { 
        player.playerQuests.ToggleQuestLog();
    }


    void HandlePlayerQuestAccept(EntityWorldQuestStateChangedEvent evt) {
        player.playerQuests.ForceRescanNearby();
    }

    


}