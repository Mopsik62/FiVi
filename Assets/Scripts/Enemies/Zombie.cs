using UnityEngine;
using System.Collections;

public class Zombie : Enemy
{
    [SerializeField]
    private ZombieDamage _damageSound;
    [SerializeField]
    private ZombieDeath _deathSound;
    [SerializeField]
    private ZombieWalk _walkSound;

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
        _walkSound.ZombieWalkEvent.Stop(gameObject);
        base.Death();
        _deathSound.ZombieDeathEvent.Post(gameObject);
    }

    public override void ReciveDamage(Damage dmg)
    {
        base.ReciveDamage(dmg);

        _damageSound.ZombieDamageEvent.Post(gameObject);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        _footsteps_timer += Time.fixedDeltaTime;
        if (_footsteps_timer >= _footsteps_cooldown)
        {
            _walkSound.ZombieWalkEvent.Post(gameObject);
            _footsteps_timer = 0;
        }
    }

}
