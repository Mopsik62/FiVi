using System.Collections;
using UnityEngine;

public class Furnace : Enemy
{
    [SerializeField]
    private float _specialDuration = 2f;
    [SerializeField]
    private float _specialDistance = 4f;
    [SerializeField]
    private GameObject Circle;
    [SerializeField]
    private PechkaDamage _damageSound;
    [SerializeField]
    private PechkaDeath _deathSound;
    [SerializeField]
    private PechkaFireAttak _specialSound;
    [SerializeField]
    private PechkaWalk _walkSound;

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
            Debug.Log("Start Dashing");
            _lastSpecial = Time.time;
            StartCoroutine(SpecialAttack());
        }
        
                _footsteps_timer += Time.fixedDeltaTime;
                if (_footsteps_timer >= _footsteps_cooldown)
                {
                    _walkSound.PechkaWalkEvent.Post(gameObject);
                    _footsteps_timer = 0;
                }


    }

    protected override void Death()
    {
        _walkSound.PechkaWalkEvent.Stop(gameObject);
        base.Death();
        _deathSound.PechkaDeathEvent.Post(gameObject);
    }

    public override void ReciveDamage(Damage dmg)
    {
        base.ReciveDamage(dmg);

        _damageSound.PechkaDamageEvent.Post(gameObject);
    }

    private IEnumerator SpecialAttack()
    {
        _isCasting = true;

        anim.SetBool("Special", true);

        Circle.transform.localScale = Vector3.one * 0.3f;
        
        Circle.SetActive(true);

        float elapsed = 0f;
        Vector3 startScale = Vector3.one * 0.3f;
        Vector3 endScale = Vector3.one * 0.9f;
        float rotationSpeed = 45f;

        _specialSound.PechkaFireAttakEvent.Post(gameObject);

        while (elapsed < _specialDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _specialDuration);

            Circle.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

            Circle.transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }
        _specialSound.PechkaFireAttakEvent.Stop(gameObject);
        Circle.SetActive(false);
        anim.SetBool("Special", false);
        _isCasting = false;
    }
}
