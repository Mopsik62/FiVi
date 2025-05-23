using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
    private TrainRail _trainSound;


    [SerializeField]
    private TextMeshProUGUI _currentScore;
    [SerializeField]
    private TextMeshProUGUI _currentWave;
    [SerializeField]
    private GameObject _chooseCanvas;
    [SerializeField]
    private int _CurrentScoreInt;
    [SerializeField]
    private GameObject _line;

    [SerializeField]
    private PolygonCollider2D _spawnAreaCollider;

    public int MaxWaves;
    public int CurWave = 0;

    [SerializeField]
    private Light2D globalLight;
    [SerializeField]
    private Light2D _playerLight;


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
        _playerLight = GameObject.FindGameObjectWithTag("Player").GetComponent<Light2D>();
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
        _trainSignal.TrainSignalEvent.Post(Player.instance.gameObject);
        yield return new WaitForSeconds(3f);


        GameObject train = Instantiate(_train.gameObject, spawnPoint.position, Quaternion.identity);

        Vector2 direction = _leftSideTrain ? Vector2.right : Vector2.left;

        train.AddComponent<TrainMover>().Init(direction, _trainSpeed);

    }

    private void DrawLine(Transform spawnPoint, bool _leftLine)
    {
        Vector3 newPosition = spawnPoint.position + new Vector3(0, 0.4f, 0);
        GameObject lineInstance = Instantiate(_line, newPosition, Quaternion.identity);
        Destroy(lineInstance, 3f);
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
            if (CurWave == 4)
                NighTime();
            if (CurWave == 8)
                DayTime();
            ChangeWave();

            if (CurWave % 2 == 0)
            {
                _chooseCanvas.SetActive(true);
                Time.timeScale = 0f;
                yield return new WaitUntil(() => _chooseCanvas.activeSelf == false);
            }

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
    
    protected void NighTime()
    {
        _playerLight.enabled = true;
        StartCoroutine(ChangeIntensity(globalLight, 0f));
    }
    protected void DayTime()
    {
        StartCoroutine(ChangeIntensity(globalLight, 1f));
    }
    private IEnumerator ChangeIntensity(Light2D light, float _targetIntesivity)
    {
        float startIntensity = light.intensity;
        float elapsed = 0f;

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime;
            light.intensity = Mathf.Lerp(startIntensity, _targetIntesivity, elapsed / 1f);
            yield return null;
        }

        light.intensity = _targetIntesivity;
        if (light.intensity == 1f)
            _playerLight.enabled = false;

    }

    public void ContinueGame()
    {
        _chooseCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToHub()
    {
        Time.timeScale = 1f;

        GameManager.instance.GetMoney(_CurrentScoreInt);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Hub");
    }




}
