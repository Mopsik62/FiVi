using UnityEngine;

public class BottleArea : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fighter"))
        {
            Damage dmg = new()
            {
                origin = transform.position,
                damage = 2,
                pushForce = 1
            };
            other.SendMessage("ReciveDamage", dmg);
        }
    }
    private void Awake()
    {
        Destroy(gameObject, 0.1f);
    }
}
