using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    protected Animator animator;
    [SerializeField]
    protected Collider2D _boxCollider;
    [SerializeField]
    protected float _damage;
    public float cooldown;

    protected virtual void Start()
    {
        _boxCollider = GetComponent<Collider2D>();
        _boxCollider.enabled = false;
        animator = GetComponent<Animator>();
    }
    public virtual void Attack()
    {
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ÓÄÀÐÈÄË (òðèããåð)");
        if (other.CompareTag("Fighter"))
        {
            Damage dmg = new()
            {
                origin = transform.position,
                damage = _damage,
                pushForce = 1
            };
            other.SendMessage("ReciveDamage", dmg);
        }
    }


}
