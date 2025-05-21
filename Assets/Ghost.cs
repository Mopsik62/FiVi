using System.Collections;
using UnityEngine;

public class Ghost : Enemy
{
    [SerializeField]
    private float _minSpecialCharge = 5f;
    [SerializeField]
    private float _maxSpecialCharge = 9f;
    [SerializeField]
    private float _specialDuretion = 3f;
    [SerializeField]
    private SpriteRenderer _sprite;

    protected override void Awake()
    {
        base.Awake();
        _lastSpecial = Time.time;
        _specialCooldown = Random.Range(_minSpecialCharge, _maxSpecialCharge);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Time.time - _lastSpecial > _specialCooldown)
        {
            StartCoroutine(Special());
            _lastSpecial = Time.time;
            _specialCooldown = Random.Range(_minSpecialCharge, _maxSpecialCharge);
        }


    }

    private IEnumerator Special()
    {
        _boxCollider.enabled = false;
        Color color = _sprite.color;
        color.a = 0.4f; 
        _sprite.color = color;

        yield return new WaitForSeconds(_specialDuretion);

        color.a = 1f;
        _sprite.color = color;
        _boxCollider.enabled = true;

    }
}
