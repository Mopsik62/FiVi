using UnityEngine;

public class FireCircle : MonoBehaviour
{
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
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
}

