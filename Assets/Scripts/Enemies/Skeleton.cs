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

    public override void ReciveDamage(Damage dmg)
    {
        base.ReciveDamage(dmg);

        _damageSound.SkeletonDamageEvent.Post(gameObject);
    }
}
