using UnityEngine;
using System.Collections;

public class Stick : Weapon
{

    public override void Attack()
    {
        StartCoroutine(Attack(cooldown));
    }

    private IEnumerator Attack(float delay)
    {
        _boxCollider.enabled = true;
        yield return new WaitForSeconds(delay);
        _boxCollider.enabled = false;
    }
}
