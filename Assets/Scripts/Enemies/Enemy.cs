using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : Fighter
{
    protected Animator anim;
    protected bool Dyuing = false;
    public float contactDamage = 1.0f;
    public Transform playerPosition;
    public int ScoreGranted;
    public float pushForceDeal = 1.0f;
    [SerializeField]
    protected float _specialCooldown = 5.0f;
    protected float _lastSpecial;
    public float moveSpeed = 1f;
    protected Rigidbody2D _rb;

    [SerializeField]
    protected int _scoreForKill;

    protected override void Awake()
    {
        base.Awake();

        _rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (playerPosition == null)
        playerPosition = GameObject.Find("Player")?.transform;
    }
    protected virtual void Update()
    {
        if (Dyuing || playerPosition == null)
            return;
            Vector3 scale = transform.localScale;

            if (playerPosition.position.x < transform.position.x)
                scale.x = Mathf.Abs(scale.x);
            else
                scale.x = -Mathf.Abs(scale.x);

            transform.localScale = scale;
    }
    protected virtual void FixedUpdate()
    {
        if (Dyuing || playerPosition == null)
            return;

        float distance = Vector2.Distance(transform.position, playerPosition.position);
        if (distance < 0.1f)
            return;

        Vector2 direction = ((Vector2)(playerPosition.position - transform.position)).normalized;
        Vector2 moveDelta = direction * moveSpeed * Time.fixedDeltaTime;

        Vector2 targetPosition = _rb.position + moveDelta;

        targetPosition = new Vector2(
            Mathf.Round(targetPosition.x * 54f) / 54f,
            Mathf.Round(targetPosition.y * 54f) / 54f
        );

        _rb.MovePosition(targetPosition);
    }

    protected virtual void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            anim.SetTrigger("Attack");
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
            anim.SetTrigger("Attack");
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
        _rb.linearVelocity = Vector2.zero;
        BattleHandler.instance.AddPoints(ScoreGranted);
        anim.SetTrigger("Death");
        StartCoroutine(DelayedDeath(1f));
    }

    private IEnumerator DelayedDeath(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
