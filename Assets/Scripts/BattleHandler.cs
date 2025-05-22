using System.Collections;
using TMPro;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{

    public static BattleHandler instance;

    [SerializeField]
    private Enemy[] _enemies;

    [SerializeField]
    private Train _train;
    [SerializeField]
    private float _trainCD;
    [SerializeField]
    private float _trainSpeed;

    [SerializeField]
    private TextMeshProUGUI _currentScore;
    [SerializeField]
    private int _CurrentScoreInt;

    [SerializeField]
    private PolygonCollider2D _spawnAreaCollider;

    public int MaxWaves;
    public int CurWave = 0;



    [SerializeField]
    private Transform[] _trainSpawnPoints = new Transform[24];

    private Transform _spawnPoint;
    private bool _leftSideTrain = false;

    [SerializeField]
    private float _minDistanceFromPlayer = 5f;
    [SerializeField]
    private float _waveDuration = 60f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {

        StartCoroutine(TrainSpawnLoop());
        CurWave = 0;
        StartCombat();
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
        Debug.Log("spawnTrain");
        int spawnIndex = Random.Range(0, 24);
        Transform spawnPoint = _trainSpawnPoints[spawnIndex];
        _leftSideTrain = spawnIndex < 12;

        GameObject train = Instantiate(_train.gameObject, spawnPoint.position, Quaternion.identity);
        Vector2 direction = _leftSideTrain ? Vector2.right : Vector2.left;

        train.AddComponent<TrainMover>().Init(direction, _trainSpeed);

    }

    private void StartCombat()
    {
        StartCoroutine(CombatLoop());

    }

    private IEnumerator CombatLoop()
    {
        for (int i = 0; i < MaxWaves; i++)
        {
            Debug.Log("Новая волна началась " + (CurWave++));

            StartCoroutine(SpawnWaveEnemies());

            yield return new WaitForSeconds(_waveDuration + 10f); 
        }


    }

    private IEnumerator SpawnWaveEnemies()
    {
        float spawnDelay = 2f;
        float timer = 0f;

        while (timer < _waveDuration)
        {
         int randomSpawnIndex = Random.Range(0, CurWave);
         Vector2 spawnPos = GetRandomPointInSpawnArea();
         Instantiate(_enemies[randomSpawnIndex], spawnPos, Quaternion.identity);
         yield return new WaitForSeconds(spawnDelay);
         timer += spawnDelay;        
        }

    }

    private Vector2 GetRandomPointInSpawnArea()
    {
        Bounds bounds = _spawnAreaCollider.bounds;
        Vector2 point;

        int safetyCounter = 0;
        do
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            point = new Vector2(x, y);

            safetyCounter++;
            if (safetyCounter > 50)
            {
                Debug.LogWarning("Не удалось найти точку в области спавна.");
                break;
            }
        } while (!_spawnAreaCollider.OverlapPoint(point) || Vector2.Distance(point, Player.instance.gameObject.transform.position) < _minDistanceFromPlayer);

        return point;
    }

    public void AddPoints(int point)
    {
        _CurrentScoreInt = int.Parse(_currentScore.text);
        _CurrentScoreInt += point;
        _currentScore.text = _CurrentScoreInt.ToString();
    }

}
