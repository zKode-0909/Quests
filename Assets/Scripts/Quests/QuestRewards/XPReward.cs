using UnityEngine;

public class XPReward : QuestReward
{
    [SerializeField] private int amount;

    public int Amount => amount;

    public XPReward(int amount) { this.amount = amount; }
    public XPReward() { } // optional, mainly for Unity/serialization comfort

    public override void GrantReward(IQuester quester) => Debug.Log("granting XP reward");//quester.AddXP(amount);


}
