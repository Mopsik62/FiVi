using UnityEngine;
using System.Collections;

public class Bottle : Weapon
{
    [SerializeField]
    private BottleProjectile _projectile;
    public override void Attack()
    {
       Instantiate(_projectile, Player.instance.transform.position , Quaternion.identity);
    }


}
