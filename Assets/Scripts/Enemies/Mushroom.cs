using UnityEngine;

public class Mushroom : Enemy
{
    [SerializeField]
    private MushroomBlow _attackSound;
    [SerializeField]
    private MushroomStart _walkSound;

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
        _walkSound.MushroomStartEvent.Stop(gameObject);
        base.Death();
        _attackSound.MushroomBlowEvent.Post(gameObject);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        _footsteps_timer += Time.fixedDeltaTime;
        if (_footsteps_timer >= _footsteps_cooldown)
        {
            _walkSound.MushroomStartEvent.Post(gameObject);
            _footsteps_timer = 0;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D coll)
    {
        base.OnCollisionEnter2D(coll);
        if (coll.gameObject.name == "Player")
        {
            Death();
        }

    }


}
