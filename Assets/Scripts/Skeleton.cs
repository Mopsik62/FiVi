using UnityEngine;
using System.Collections;

public class Skeleton : Fighter
{
    [SerializeField]
    private Skeleton_Damage _damageSound;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    protected override void Death()
    {
        base.Death();
        _boxCollider.enabled = false;
        animator.SetTrigger("Death");
        StartCoroutine(DelayedDeath(1f));
    }

    public override void ReciveDamage(Damage dmg)
    {
        base.ReciveDamage(dmg);

        _damageSound.SkeletonDamageEvent.Post(gameObject);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.name);
        if (collision.collider.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");
            Damage dmg = new()
            {
                origin = transform.position,
                damage = 1,
                pushForce = 1
            };
            collision.collider.SendMessage("ReciveDamage", dmg);
        }
    }
    private IEnumerator DelayedDeath(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
