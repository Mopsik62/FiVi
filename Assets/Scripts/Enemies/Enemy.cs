using System.Collections;
using UnityEngine;

public class Enemy : Fighter
{
    protected Animator anim;
    protected bool Dyuing = false;
    public float contactDamage = 1.0f;
    public Transform playerPosition;
    public float ScoreGranted;
    public float pushForceDeal = 1.0f;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            //anim.SetTrigger("Attack");
            Damage dmg = new()
            {
                origin = transform.position,
                damage = contactDamage,
                pushForce = pushForceDeal
            };
            coll.gameObject.SendMessage("ReciveDamage", dmg);
        }

    }
    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            //anim.SetTrigger("Attack");
            Damage dmg = new()
            {
                origin = transform.position,
                damage = contactDamage,
                pushForce = pushForceDeal
            };
            coll.gameObject.SendMessage("ReciveDamage", dmg);
        }

    }

    protected override void Death()
    {
        Dyuing = true;
        _boxCollider.enabled = false;
        anim.SetTrigger("Death");
        StartCoroutine(DelayedDeath(1f));
    }
    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        if (playerPosition == null)
        playerPosition = GameObject.Find("Player")?.transform;
    }

    private IEnumerator DelayedDeath(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
