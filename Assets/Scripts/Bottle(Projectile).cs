using UnityEngine;
using System.Collections;

public class BottleProjectile : Weapon
{

    [SerializeField]
    private BottleBlast _damageSound;

    private Vector2 _direction;
    private float rotationSpeed = 360;
    private float _speed = 8f; 
    [SerializeField] private Transform _sprite;
    [SerializeField] private GameObject _area;

    private Vector3 _targetPosition;
    private bool _hasReachedTarget = false;

    protected override void Start()
    {
        base.Start();

        _boxCollider = GetComponent<Collider2D>();

        _targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _targetPosition.z = 0f;

        _direction = (_targetPosition - transform.position).normalized;

        Destroy(gameObject, 3f);
    }

    void Update()
    {
        _sprite.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        transform.position += (Vector3)(_direction * _speed * Time.deltaTime);

        if (!_hasReachedTarget && Vector2.Distance(Player.instance.transform.position, _targetPosition) < 0.1f)
        {
            Instantiate(_area, _targetPosition, Quaternion.identity);
            _hasReachedTarget = true;
            _damageSound.BottleBlastEvent.Post(gameObject);
            Destroy(gameObject); 
        }
    }

}
