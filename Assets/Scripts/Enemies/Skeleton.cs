using UnityEngine;
using System.Collections;

public class Skeleton : Enemy
{
    [SerializeField]
    private Skeleton_Damage _damageSound;
    [SerializeField]
    private Skeleton_death _skeletonDeath;


    protected override void Death()
    {
        base.Death();
        _skeletonDeath.SkeletonDeathEvent.Post(gameObject);
    }

    private void Update()
    {
        if (Dyuing)
            return;
        if (playerPosition != null)
        {
            Vector3 scale = transform.localScale;

            if (playerPosition.position.x < transform.position.x)
                scale.x = Mathf.Abs(scale.x);
            else
                scale.x = -Mathf.Abs(scale.x);

            transform.localScale = scale;
        }
    }

    public override void ReciveDamage(Damage dmg)
    {
        base.ReciveDamage(dmg);

        _damageSound.SkeletonDamageEvent.Post(gameObject);
    }
}
