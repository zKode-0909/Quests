using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInputReader input;
    [SerializeField] Player player;

    Rigidbody rb;

    EventBinding<TestEvent> testEventBinding;
    EventBinding<PlayerEvent> playerEventBinding;

    EventBinding<EntityWorldQuestStateChangedEvent> worldQuestStateChangeBinding;

    public Transform Transform => this.transform;

    public GameObject GameObject => this.gameObject;

    public string ID => player.ID;

    void Awake() { 
    
    }

    void OnEnable()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        input.MoveEvent += HandleMove;
        input.AttackEvent += HandleAttack;
        input.OpenQuestLogEvent += HandleOpenQuestLog;

        input.PressTEvent += HandleTPress;
        input.PressYEvent += HandleYPress;

        input.OpenInventoryEvent += HandleOpenInventory;

        testEventBinding = new EventBinding<TestEvent>(HandleTestEvent);
        EventBus<TestEvent>.Register(testEventBinding);

        playerEventBinding = new EventBinding<PlayerEvent>(HandlePlayerEvent);
        EventBus<PlayerEvent>.Register(playerEventBinding);

        worldQuestStateChangeBinding = new EventBinding<EntityWorldQuestStateChangedEvent>(HandlePlayerQuestAccept);
        EventBus<EntityWorldQuestStateChangedEvent>.Register(worldQuestStateChangeBinding);
    }

    private void OnDisable()
    {
        input.MoveEvent -= HandleMove;
        input.AttackEvent -= HandleAttack;
        input.OpenQuestLogEvent -= HandleOpenQuestLog;

        input.PressTEvent -= HandleTPress;
        input.PressYEvent -= HandleYPress;

        input.OpenInventoryEvent -= HandleOpenInventory;

        EventBus<TestEvent>.Deregister(testEventBinding);
        EventBus<PlayerEvent>.Deregister(playerEventBinding);
        EventBus<EntityWorldQuestStateChangedEvent>.Deregister(worldQuestStateChangeBinding);
    }



    // Update is called once per frame
    void Update()
    {

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
    /*
    public void HandleAcceptQuest(IQuester quester,Quest quest) {
        player.playerQuests.TryAcceptQuest(quest);
    }*/

    void HandleOpenQuestLog() { 
        player.playerQuests.ToggleQuestLog();
    }

    void HandleTestEvent()
    {
        Debug.Log("Test event received");
    }

    void HandlePlayerEvent(PlayerEvent playerEvent)
    {
        Debug.Log($"Player event received! XP: {playerEvent.xp}");
    }

    void HandleYPress() {
        EventBus<PlayerEvent>.Raise(new PlayerEvent {
            xp = player.playerLevelling.xpToLevel
        });
    }

    void HandleTPress() {
        EventBus<TestEvent>.Raise(new TestEvent());
    }

    void HandlePlayerQuestAccept(EntityWorldQuestStateChangedEvent evt) {
        player.playerQuests.ForceRescanNearby();
       // player.QuestLog.TryAddQuest(evt.QuestID);
    }

    


}