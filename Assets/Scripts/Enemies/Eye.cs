using UnityEngine;

public class Eye : Enemy
{
    [SerializeField]
    private EyeDamage _damageSound;

    [SerializeField]
    private EyeRoll _walkSound;
    [SerializeField]
    private float _footsteps_cooldown;
    private float _footsteps_timer;

    public float rotationSpeed = 180f;


    protected override void Awake()
    {
        base.Awake();
        _lastSpecial = Time.time;
        _footsteps_timer = _footsteps_cooldown;

    }

    protected override void FixedUpdate()
    {
        if (Dyuing)
            return;
        base.FixedUpdate();

        int direction = playerPosition.position.x < transform.position.x ? 1 : -1;

        transform.Rotate(0f, 0f, direction * rotationSpeed * Time.deltaTime);

        _footsteps_timer += Time.fixedDeltaTime;
        if (_footsteps_timer >= _footsteps_cooldown)
        {
            _walkSound.EyeRollEvent.Post(gameObject);
            _footsteps_timer = 0;
        }


    }
    protected override void Death()
    {
        base.Death();

        _walkSound.EyeRollEvent.Stop(gameObject);
    }

    public override void ReciveDamage(Damage dmg)
    {
        base.ReciveDamage(dmg);

        _damageSound.EyeDamageEvent.Post(gameObject);
    }

}
