using UnityEngine;

public class Eye : Enemy
{
    public float rotationSpeed = 180f;

    protected override void FixedUpdate()
    {
        if (Dyuing)
            return;
        base.FixedUpdate();

        int direction = playerPosition.position.x < transform.position.x ? 1 : -1;

        transform.Rotate(0f, 0f, direction * rotationSpeed * Time.deltaTime);


    }
}
