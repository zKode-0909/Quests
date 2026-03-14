using UnityEngine;

public class SimPlayerMotor : PlayerMotor
{
    public override void Move()
    {
        Debug.Log("I am on the move");
    }

    public override void SetMovementDir(Vector2 dir)
    {
        Debug.Log($"my dir is {dir}");
    }
}
