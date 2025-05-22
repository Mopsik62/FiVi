using UnityEngine;

public class Fighter : MonoBehaviour
{
    //HP BASICS
    public float curHp = 1;
    public float maxHp = 1;
    [SerializeField]
    protected Collider2D _boxCollider;
    //KNOCKBACK BASICS
    protected Vector3 pushDirection;
    public float pushRecoverySpeed = 1f;

    //IMMUNE BASICS
    public float immuneTime = 1f;
    protected float lastImmune;
    protected bool CanTakeDamage => Time.time - lastImmune > immuneTime;

    protected virtual void Awake()
    {
        lastImmune = Time.time;
        _boxCollider = GetComponent<Collider2D>();
    }
    public virtual void ReciveDamage(Damage dmg)
    {
        if (CanTakeDamage)
        {
            Debug.Log("HIT " + this.name);
            lastImmune = Time.time;
            curHp -= dmg.damage;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;
            if (curHp <= 0)
            {
                Death();
            }
        }

    }
    protected virtual void Death()
    {
        Debug.Log("DEATH OF THE " + this.name);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Train"))
        {
            Damage dmg = new()
            {
                origin = transform.position,
                damage = 5,
                pushForce = 1
            };
            this.SendMessage("ReciveDamage", dmg);
        }
    }
}
