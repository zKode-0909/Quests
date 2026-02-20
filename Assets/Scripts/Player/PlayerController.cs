using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInputReader input;
    [SerializeField] Player player;

    EventBinding<TestEvent> testEventBinding;
    EventBinding<PlayerEvent> playerEventBinding;

    EventBinding<EntityAcceptQuest> acceptQuestBinding;

    public Transform Transform => this.transform;

    public GameObject GameObject => this.gameObject;

    public string ID => player.ID;

    void Awake() { 
    
    }

    void OnEnable()
    {
        input.MoveEvent += HandleMove;
        input.AttackEvent += HandleAttack;
        input.OpenQuestLogEvent += HandleOpenQuestLog;

        input.PressTEvent += HandleTPress;
        input.PressYEvent += HandleYPress;

        testEventBinding = new EventBinding<TestEvent>(HandleTestEvent);
        EventBus<TestEvent>.Register(testEventBinding);

        playerEventBinding = new EventBinding<PlayerEvent>(HandlePlayerEvent);
        EventBus<PlayerEvent>.Register(playerEventBinding);

        acceptQuestBinding = new EventBinding<EntityAcceptQuest>(HandlePlayerQuestAccept);
        EventBus<EntityAcceptQuest>.Register(acceptQuestBinding);
    }

    private void OnDisable()
    {
        input.MoveEvent -= HandleMove;
        input.AttackEvent -= HandleAttack;
        input.OpenQuestLogEvent -= HandleOpenQuestLog;

        input.PressTEvent -= HandleTPress;
        input.PressYEvent -= HandleYPress;

        EventBus<TestEvent>.Deregister(testEventBinding);
        EventBus<PlayerEvent>.Deregister(playerEventBinding);
        EventBus<EntityAcceptQuest>.Deregister(acceptQuestBinding);
    }



    // Update is called once per frame
    void Update()
    {

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

    void HandlePlayerQuestAccept(EntityAcceptQuest evt) {
        Debug.Log($"Trying to accept quest for player {evt.EntityRuntimeID}, quest is: {evt.QuestID}");
        player.playerQuests.ForceRescanNearby();
        Debug.Log("YO NIGGA, I JUST ACCEPTED A QUEST");
       // player.QuestLog.TryAddQuest(evt.QuestID);
    }

    


}