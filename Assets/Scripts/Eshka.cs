using UnityEngine;
using System.Collections;

public class Eshka : Weapon
{
    [SerializeField]
    private EshkaProjectile _projectile;
    public override void Attack()
    {
       Instantiate(_projectile, transform.position , Quaternion.identity);
    }


}
