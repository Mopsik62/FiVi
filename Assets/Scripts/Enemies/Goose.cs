using UnityEngine;

public class Goose : Enemy
{
    [SerializeField]
    private GooseDamage _damageSound;
    [SerializeField]
    private GooseDeath _deathSound;
    [SerializeField]
    private GooseAttak _attackSound;
    [SerializeField]
    private GooseWalk _walkSound;

    [SerializeField]
    private float _footsteps_cooldown;
    private float _footsteps_timer;

    protected override void Awake()
    {
        base.Awake();
        _footsteps_timer = _footsteps_cooldown;
    }
    protected override void Death()
    {
        _walkSound.GooseWalkEvent.Stop(gameObject);
        base.Death();
        _deathSound.GooseDeathEvent.Post(gameObject);
    }

    protected override void Update()
    {

        base.Update();
/*        Vector3 scale = transform.localScale;

        if (playerPosition.position.x < transform.position.x)
            scale.x = -Mathf.Abs(scale.x);
        else
            scale.x = Mathf.Abs(scale.x);
        transform.localScale = scale;*/
    }

    public override void ReciveDamage(Damage dmg)
    {
        base.ReciveDamage(dmg);

        _damageSound.GooseDamageEvent.Post(gameObject);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        _footsteps_timer += Time.fixedDeltaTime;
        if (_footsteps_timer >= _footsteps_cooldown)
        {
            _walkSound.GooseWalkEvent.Post(gameObject);
            _footsteps_timer = 0;
        }
    }
    protected override void OnCollisionEnter2D(Collision2D coll)
    {
        base.OnCollisionEnter2D(coll);
        if (coll.gameObject.name == "Player")
        {
            _attackSound.GooseAttakEvent.Post(gameObject);
        }

    }
}
