using System.Collections;
using UnityEngine;

public class Granny : Enemy
{
    [SerializeField]
    private float _specialDuration = 1.5f;
    [SerializeField]
    private GameObject _mushroom;
    [SerializeField]
    private Transform _summonSpawnPoint;
    private bool _isSummoning = false;

    protected override void Update()
    {

        base.Update();
        Vector3 scale = transform.localScale;

        if (playerPosition.position.x < transform.position.x)
            scale.x = -Mathf.Abs(scale.x);
        else
            scale.x = Mathf.Abs(scale.x);
        transform.localScale = scale;
    }



    protected override void Awake()
    {
        base.Awake();
        _lastSpecial = Time.time;
    }

    protected override void FixedUpdate()
    {
        if (_isSummoning)
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }
        base.FixedUpdate();

        if (Time.time - _lastSpecial > _specialCooldown)
        {
            StartCoroutine(Special());
            _lastSpecial = Time.time;
        }


    }

    private IEnumerator Special()
    {
        _isSummoning = true;
        _rb.linearVelocity = Vector2.zero;
        anim.SetBool("Special", true);
        yield return new WaitForSeconds(_specialDuration);

        anim.SetBool("Special", false);


        _isSummoning = false;

    }
    public void SpawnMushroom()
    {
        Instantiate(_mushroom, _summonSpawnPoint.position, Quaternion.identity);

    }
}
