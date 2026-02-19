using UnityEngine;

public class GoldReward : QuestReward
{
    [SerializeField] private int amount;

    public int Amount => amount;

    public GoldReward(int amount) { this.amount = amount; }
    public GoldReward() { } // optional, mainly for Unity/serialization comfort

    public override void GrantReward(IQuester quester) => Debug.Log("granting Gold reward");//quester.AddXP(amount);
}
