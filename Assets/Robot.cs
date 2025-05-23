using System.Collections;
using UnityEngine;

public class Robot : Enemy
{
    [SerializeField]
    private float _specialDuration = 3f;

    [SerializeField]
    private float _specialDistance = 15f;
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private RobotDamage _damageSound;
    [SerializeField]
    private RobotDeath _deathSound;
    [SerializeField]
    private RobotLaserAttak _specialSound;
    [SerializeField]
    private RobotFootstep _walkSound;

    [SerializeField]
    private float _footsteps_cooldown;
    private float _footsteps_timer;

    private bool _isCasting = false;

    protected override void Awake()
    {
        base.Awake();
        _lastSpecial = Time.time;
        _footsteps_timer = _footsteps_cooldown;

    }
    protected override void Update()
    {

        base.Update();
        Vector3 scale = transform.localScale;

        if (playerPosition.position.x < transform.position.x)
            scale.x = -Mathf.Abs(scale.x);
        else
            scale.x = Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    protected override void FixedUpdate()
    {
        if (_isCasting)
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }
        base.FixedUpdate();
        float distance = Vector2.Distance(transform.position, playerPosition.position);
        if ((distance < _specialDistance) && (Time.time - _lastSpecial > _specialCooldown))
        {
            _lastSpecial = Time.time;
            StartCoroutine(SpecialAttack());
        }

        _footsteps_timer += Time.fixedDeltaTime;
        if (_footsteps_timer >= _footsteps_cooldown)
        {
            _walkSound.RobotFootstepsEvent.Post(gameObject);
            _footsteps_timer = 0;
        }


    }

    protected override void Death()
    {
        _walkSound.RobotFootstepsEvent.Stop(gameObject);
        base.Death();
        _deathSound.RobotDeathEvent.Post(gameObject);
    }

    public override void ReciveDamage(Damage dmg)
    {
        base.ReciveDamage(dmg);

        _damageSound.RobotDamageEvent.Post(gameObject);
    }

    private IEnumerator SpecialAttack()
    {
        _isCasting = true;

        anim.SetBool("Special", true);

        float elapsed = 0f;

        while (elapsed < _specialDuration)
        {
            Instantiate(_laser, transform.position, Quaternion.identity);
            _specialSound.RobotAttakEvent.Post(gameObject);
            yield return new WaitForSeconds(0.5f);
            elapsed += 0.5f;
        }

      
        anim.SetBool("Special", false);
        _isCasting = false;
    }
}
