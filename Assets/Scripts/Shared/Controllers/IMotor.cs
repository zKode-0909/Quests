using UnityEngine;

public interface IMotor
{
    public void Move();
    public void SetMovementDir(Vector2 dir);
}
