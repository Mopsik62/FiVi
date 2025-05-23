using UnityEngine;
using System.Collections;

public class Bottle : Weapon
{
    [SerializeField]
    private BottleProjectile _projectile;
    public override void Attack()
    {
       Instantiate(_projectile, transform.position , Quaternion.identity);
    }


}
