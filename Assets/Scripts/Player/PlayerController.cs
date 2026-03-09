using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerInputReader input;
    Player player;


    Rigidbody rb;


    EventBinding<EntityWorldQuestStateChangedEvent> worldQuestStateChangeBinding;



    public void Initialize(Rigidbody rb,PlayerInputReader input,Player player)
    {
        this.rb = rb;
        this.input = input;
        this.player = player;
        rb.freezeRotation = true;
        input.MoveEvent += HandleMove;
        input.AttackEvent += HandleAttack;
        input.OpenQuestLogEvent += HandleOpenQuestLog;


        input.OpenInventoryEvent += HandleOpenInventory;

        worldQuestStateChangeBinding = new EventBinding<EntityWorldQuestStateChangedEvent>(HandlePlayerQuestAccept);
        EventBus<EntityWorldQuestStateChangedEvent>.Register(worldQuestStateChangeBinding);
    }


    private void OnDisable()
    {
        input.MoveEvent -= HandleMove;
        input.AttackEvent -= HandleAttack;
        input.OpenQuestLogEvent -= HandleOpenQuestLog;

        input.OpenInventoryEvent -= HandleOpenInventory;
        EventBus<EntityWorldQuestStateChangedEvent>.Deregister(worldQuestStateChangeBinding);
    }

    void HandleOpenInventory() {
        Debug.Log("received open inventory input");
        player.playerInventory.ToggleInventory(player.entityRuntimeID);
    }

    void HandleMove(Vector2 dir)
    {
        player.motor.SetMovementDir(dir);
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