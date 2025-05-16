using UnityEngine;
using Cinemachine;
using System.Collections;

public class Boostrap : MonoBehaviour
{

    public CinemachineVirtualCamera virtualCam;
    public GameObject boundaryObject;

    void Start()
    {
        StartCoroutine(SetupConfinerDelayed());
    }

    IEnumerator SetupConfinerDelayed()
    {
        yield return null;

        if (virtualCam == null)
            virtualCam = FindObjectOfType<CinemachineVirtualCamera>();

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
