using System.Collections;
using UnityEngine;

public class Ghost : Enemy
{
    [SerializeField]
    private float _minSpecialCharge = 5f;
    [SerializeField]
    private float _maxSpecialCharge = 9f;
    [SerializeField]
    private float _specialDuretion = 3f;
    [SerializeField]
    private SpriteRenderer _sprite;

    [SerializeField]
    private float _footsteps_cooldown;
    private float _footsteps_timer;

    [SerializeField]
    private GhostDamage _damageSound;
    [SerializeField]
    private GhostAttak _atackSound;
    /*    [SerializeField]
        private ZombieDeath _deathSound;*/
    [SerializeField]
    private GhostWalk _walkSound;


    protected override void Awake()
    {
        base.Awake();
        _lastSpecial = Time.time;
        _footsteps_timer = _footsteps_cooldown;
        _specialCooldown = Random.Range(_minSpecialCharge, _maxSpecialCharge);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Time.time - _lastSpecial > _specialCooldown)
        {
            StartCoroutine(Special());
            _lastSpecial = Time.time;
            _specialCooldown = Random.Range(_minSpecialCharge, _maxSpecialCharge);
        }

        _footsteps_timer += Time.fixedDeltaTime;
        if (_footsteps_timer >= _footsteps_cooldown)
        {
            _walkSound.GhostWalkEvent.Post(gameObject);
            _footsteps_timer = 0;
        }

    }

    protected override void OnCollisionEnter2D(Collision2D coll)
    {
        base.OnCollisionEnter2D(coll);
        if (coll.gameObject.name == "Player")
        {
           // _atackSound.GhostAttakEvent.Post(gameObject);
        }

    }

    protected override void Death()
    {
        _walkSound.GhostWalkEvent.Stop(gameObject);
        base.Death();
       // _deathSound.ZombieDeathEvent.Post(gameObject);
    }

    public override void ReciveDamage(Damage dmg)
    {
        base.ReciveDamage(dmg);

        _damageSound.GhostDamageEvent.Post(gameObject);
    }

    private IEnumerator Special()
    {
        _boxCollider.enabled = false;
        Color color = _sprite.color;
        color.a = 0.4f; 
        _sprite.color = color;

        yield return new WaitForSeconds(_specialDuretion);

        color.a = 1f;
        _sprite.color = color;
        _boxCollider.enabled = true;

    }
}
