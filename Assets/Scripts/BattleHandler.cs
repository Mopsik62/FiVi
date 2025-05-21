using System.Collections;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    [SerializeField]
    private Enemy[] _enemies;

    [SerializeField]
    private Train _train;
    [SerializeField]
    private float _trainCD;
    [SerializeField]
    private float _trainSpeed;


    [SerializeField]
    private Transform[] _trainSpawnPoints = new Transform[24];

    private Transform _spawnPoint;
    private bool _leftSideTrain = false;

    private void Start()
    {

        StartCoroutine(TrainSpawnLoop());
        //Debug.Log(GameManager.instance.CurrentMoney);
    }
    private IEnumerator TrainSpawnLoop()
    {
        while (true)
        {
            SpawnTrain();
            yield return new WaitForSeconds(_trainCD); 
        }
    }
    private void SpawnTrain()
    {
        int spawnIndex = Random.Range(0, 24);
        Transform spawnPoint = _trainSpawnPoints[spawnIndex];
        _leftSideTrain = spawnIndex < 12;

        GameObject train = Instantiate(_train.gameObject, spawnPoint.position, Quaternion.identity);
        Vector2 direction = _leftSideTrain ? Vector2.right : Vector2.left;

        train.AddComponent<TrainMover>().Init(direction, _trainSpeed);

    }

}
