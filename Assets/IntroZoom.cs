using UnityEngine;
using Cinemachine;

public class IntroZoom : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;
    public float zoomSpeed = 1.5f;
    public float targetSize = 2f;
    public float delayBeforeLoad = 2f;
    public string nextScene;

    private bool zooming = true;

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
                Invoke(nameof(LoadNextScene), delayBeforeLoad);
            }
        }
    }

    void LoadNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
    }
}
