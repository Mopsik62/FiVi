using System.Collections;
using UnityEngine;

public class Shade : Enemy
{
    [SerializeField]
    private float _specialDuration = 3f;

    [SerializeField]
    private Enemy[] _spawnedEnemies;

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
            SpawnEnemy();
            _lastSpecial = Time.time;
        }


    }

/*    private IEnumerator Special()
    {
        _isSummoning = true;
        _rb.linearVelocity = Vector2.zero;
        //anim.SetBool("Special", true);
        yield return new WaitForSeconds(_specialDuration);

        //anim.SetBool("Special", false);


        _isSummoning = false;

    }*/
    public void SpawnEnemy()
    {
        _isSummoning = true;
        _rb.linearVelocity = Vector2.zero;
        StartCoroutine(SpawnEnemiesWithDelay());

    }

    private IEnumerator SpawnEnemiesWithDelay()
    {
        for (int i = 0; i < 3; i++)
        {
            float randomValue = Random.value;

            GameObject enemyToSpawn;

            if (randomValue < 0.7f)
                enemyToSpawn = _spawnedEnemies[0].gameObject;
            else if (randomValue < 0.9f)
                enemyToSpawn = _spawnedEnemies[1].gameObject;
            else
                enemyToSpawn = _spawnedEnemies[2].gameObject;

            Debug.Log(enemyToSpawn.name);

            Instantiate(enemyToSpawn, _summonSpawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(1f);
        }
        _isSummoning = false;
    }
}
