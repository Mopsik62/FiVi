using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering.Universal;
using AK.Wwise;


public class IntroZoom : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;
    public float zoomSpeed = 1.5f;
    public float targetSize = 2f;
    public float delayBeforeZoom = 1f;
    public float delayBeforeNewScene = 1f;
    public string nextScene;

    public Light2D light1, light2;
    public float flickerDuration = 0.5f;    
    public int maxFlickers = 3;            
    private int flickerCount = 0;
    private float flickerTimer = 0f;
    private bool lightsOn = true;
    private float baseIntensity1;
    private float baseIntensity2;

    private bool zooming = false;

    [SerializeField]
    private TitleMusic _titleMusic;


    void Start()
    {
        baseIntensity1 = light1.intensity;
        baseIntensity2 = light2.intensity;
        Invoke(nameof(StartZoom), delayBeforeZoom);
        _titleMusic.TitleMusiclEvent.Post(gameObject);

    }

    void StartZoom()
    {
        zooming = true;
    }

    void Update()
    {
        if (zooming)
        {
            float currentSize = virtualCam.m_Lens.OrthographicSize;
            if (currentSize > targetSize)
            {
                virtualCam.m_Lens.OrthographicSize = Mathf.MoveTowards(currentSize, targetSize, zoomSpeed * Time.deltaTime);
            }
            else
            {
                zooming = false;
                Invoke(nameof(LoadNextScene), delayBeforeNewScene);
            }
        }

        if (flickerCount < maxFlickers)
        {
            flickerTimer += Time.deltaTime;
            if (flickerTimer >= flickerDuration / 2f)
            {
                flickerTimer = 0f;
                lightsOn = !lightsOn;
                if (!lightsOn) flickerCount++;
            }

            SetLightsIntensity(lightsOn ? baseIntensity1 : baseIntensity1 * 0.1f,
                               lightsOn ? baseIntensity2 : baseIntensity2 * 0.1f);
        }
        else
        {
            SetLightsIntensity(0f, 0f);
        }
    }
    void SetLightsIntensity(float intensity1, float intensity2)
    {
        if (light1 != null) light1.intensity = intensity1;
        if (light2 != null) light2.intensity = intensity2;
    }
    void LoadNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
    }
}
