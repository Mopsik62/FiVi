using UnityEngine;
using System.Collections;

public class Skeleton : Enemy
{
    [SerializeField]
    private Skeleton_Damage _damageSound;
    [SerializeField]
    private Skeleton_death _skeletonDeath;
    [SerializeField]
    private SkeletonWalk _walkSound;

    [SerializeField]
    private float _footsteps_cooldown;
    private float _footsteps_timer;


    protected override void Awake()
    {
        base.Awake();
        _lastSpecial = Time.time;
        _footsteps_timer = _footsteps_cooldown;

    }

    protected override void Death()
    {
        base.Death();

        _skeletonDeath.SkeletonDeathEvent.Post(gameObject);
    }

    protected override void FixedUpdate()
    {

        base.FixedUpdate();
        _footsteps_timer += Time.fixedDeltaTime;
        if (_footsteps_timer >= _footsteps_cooldown)
        {
            _walkSound.SkeletonWalkEvent.Post(gameObject);
            _footsteps_timer = 0;
        }


    }

    public override void ReciveDamage(Damage dmg)
    {
        base.ReciveDamage(dmg);

        _damageSound.SkeletonDamageEvent.Post(gameObject);
    }
}
