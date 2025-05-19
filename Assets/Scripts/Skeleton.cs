using UnityEngine;
using System.Collections;

public class Skeleton : Fighter
{
    [SerializeField]
    private Skeleton_Damage _damageSound;
    private Animator animator;
    private Transform player;
    private bool Dyuing = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }
    protected override void Death()
    {
        base.Death();
        Dyuing = true;
        _boxCollider.enabled = false;
        animator.SetTrigger("Death");
        StartCoroutine(DelayedDeath(1f));
    }

    private void Update()
    {
        if (Dyuing)
            return;
        if (player != null)
        {
            Vector3 scale = transform.localScale;

            if (player.position.x < transform.position.x)
                scale.x = Mathf.Abs(scale.x);
            else
                scale.x = -Mathf.Abs(scale.x);

            transform.localScale = scale;
        }
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
