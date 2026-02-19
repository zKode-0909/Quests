using UnityEngine;

public class PlayerMotor 
{
    public Vector2 movementDir = Vector2.zero;
    Rigidbody rb;
    Transform transform;
    Camera camera;
    float speed;
    float currentSpeed = 0;
    float rotationSpeed = 360f;
    float smoothTime = 0.2f;
    float velocity;

    public PlayerMotor(Rigidbody rb_, Transform trns,float speed_,Camera cam) { 
        rb = rb_;
        transform = trns;
        speed = speed_;
        camera = cam;
    }

    public void Move()
    {

        if (movementDir == Vector2.zero)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            return;
        }

        // Camera-relative direction
        Vector3 forward = camera.transform.forward;
        Vector3 right = camera.transform.right;
        forward.y = 0f; right.y = 0f;
        forward.Normalize(); right.Normalize();

        Vector3 dir = (forward * movementDir.y + right * movementDir.x).normalized;
        if (dir.sqrMagnitude < 0.0001f) return;
        dir.Normalize();

        // Speed is already units/sec (no dt factor!)
        float moveSpeed = speed;
        rb.linearVelocity = new Vector3(dir.x * moveSpeed, rb.linearVelocity.y, dir.z * moveSpeed);

        // Rotate via rigidbody
        Quaternion targetRot = Quaternion.LookRotation(dir);
        rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime));
    }


    public void SetMovementDir(Vector2 dir) { 
        movementDir = dir;
    }

    void SmoothSpeed(float value)
    {
        currentSpeed = Mathf.SmoothDamp(currentSpeed, value, ref velocity, smoothTime);
    }

    void HandleHorizontalMovement(Vector3 dir)
    {
        var velocity = dir * (currentSpeed * UnityEngine.Time.fixedDeltaTime);
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
        Debug.Log($"moving horizontally with speed {currentSpeed}");
    }

}
