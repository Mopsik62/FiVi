using UnityEngine;
using System.Collections;

public class EshkaProjectile : Weapon
{
    private Vector2 _direction;
    private float rotationSpeed = 360;
    private float _speed = 432f;
    [SerializeField]
    private Transform _sprite;
    protected override void Start()
    {
        _boxCollider = GetComponent<Collider2D>();
        _boxCollider.enabled = true;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        _direction = ((Vector2)(mouseWorldPos - transform.position)).normalized;
        Destroy(gameObject, 3f);
    }
    void Update()
    {
        float speedUnits = _speed / 54;
        _sprite.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        transform.Translate(_direction * speedUnits * Time.deltaTime);
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.CompareTag("Fighter"))
        {
            Destroy(gameObject);
        }
    }

}
