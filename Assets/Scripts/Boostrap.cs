using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Boostrap : MonoBehaviour
{
    public static Boostrap instance;

    public CinemachineVirtualCamera virtualCam;
    public GameObject boundaryObject;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("BOOSTRAP scene loaded: " + scene.name);
        StartCoroutine(SetupScene());
    }
    IEnumerator SetupScene()
    {
        yield return null;

        if (virtualCam == null)
            virtualCam = FindObjectOfType<CinemachineVirtualCamera>();

        if (boundaryObject == null)
        {
            boundaryObject = GameObject.Find("Camera Boundaries");
            Debug.Log("Finding");
        }

        if (virtualCam != null && boundaryObject != null)
        {
            var confiner = virtualCam.GetComponent<CinemachineConfiner2D>();
            if (confiner != null)
            {
                Collider2D boundaryCollider = boundaryObject.GetComponent<Collider2D>();
                if (boundaryCollider != null)
                {
                    confiner.m_BoundingShape2D = boundaryCollider;

                    confiner.enabled = false;
                    confiner.enabled = true;
                }
                else
                {
                    Debug.LogWarning("Boundary object не содержит Collider2D!");
                }
            }
        }
    }


}
