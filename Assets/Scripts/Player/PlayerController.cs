using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInputReader input;
    [SerializeField] Player player;

    EventBinding<TestEvent> testEventBinding;
    EventBinding<PlayerEvent> playerEventBinding;

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

    


}