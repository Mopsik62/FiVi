using UnityEngine;

public class Robot_laser : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 moveDirection;

    private void Awake()
    {

        Vector2 targetPosition = Player.instance.transform.position;
        moveDirection = (targetPosition - (Vector2)transform.position).normalized;

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Destroy(gameObject, 2f);

    }

    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Damage dmg = new()
            {
                origin = transform.position,
                damage = 1,
                pushForce = 1
            };
            other.SendMessage("ReciveDamage", dmg);
        }
    }
}
