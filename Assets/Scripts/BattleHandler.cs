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
    private TrainSignal _trainSignal;
    

    [SerializeField]
    private TextMeshProUGUI _currentScore;
    [SerializeField]
    private TextMeshProUGUI _currentWave;

    [SerializeField]
    private int _CurrentScoreInt;
    [SerializeField]
    private GameObject _line;

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
        StartCoroutine(SpawnTrainWithLine(spawnIndex));

    }

    private IEnumerator SpawnTrainWithLine(int spawnIndex)
    {
        Transform spawnPoint = _trainSpawnPoints[spawnIndex];
        _leftSideTrain = spawnIndex < 12;
        DrawLine(spawnPoint, _leftSideTrain);

        yield return new WaitForSeconds(2f);


        GameObject train = Instantiate(_train.gameObject, spawnPoint.position, Quaternion.identity);
        _trainSignal.TrainSignalEvent.Post(train);

        Vector2 direction = _leftSideTrain ? Vector2.right : Vector2.left;

        train.AddComponent<TrainMover>().Init(direction, _trainSpeed);

    }

    private void DrawLine(Transform spawnPoint, bool _leftLine)
    {
        Vector3 newPosition = spawnPoint.position + new Vector3(0, 0.4f, 0);
        GameObject lineInstance = Instantiate(_line, newPosition, Quaternion.identity);
        Destroy(lineInstance, 2f);
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
            ChangeWave();
            StartCoroutine(SpawnWaveEnemies());

            yield return new WaitForSeconds(_waveDuration + 1f); 
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
                break;
            }

        } while (
            !_spawnAreaCollider.OverlapPoint(point) ||
            Vector2.Distance(point, Player.instance.transform.position) < _minDistanceFromPlayer
        );

        return point;
    }

    public void AddPoints(int point)
    {
        _CurrentScoreInt = int.Parse(_currentScore.text);
        _CurrentScoreInt += point;
        _currentScore.text = _CurrentScoreInt.ToString();
    }

    public void ChangeWave()
    {
        _currentWave.text = CurWave.ToString();
    }

}
