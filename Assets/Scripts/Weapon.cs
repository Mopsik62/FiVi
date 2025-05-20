using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    protected Collider2D _boxCollider;
    [SerializeField]
    private float _damage;
    public float cooldown;

    void Start()
    {
        _boxCollider = GetComponent<Collider2D>();
        _boxCollider.enabled = false;
        animator = GetComponent<Animator>();
    }
    public void Attack()
    {
        StartCoroutine(Attack(cooldown));
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

    private IEnumerator Attack(float delay)
    {
        _boxCollider.enabled = true;
        yield return new WaitForSeconds(delay);
        _boxCollider.enabled = false;
    }
}
