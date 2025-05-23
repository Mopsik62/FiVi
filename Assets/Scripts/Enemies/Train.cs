using System.Collections;
using UnityEngine;

public class Train : MonoBehaviour
{

    [SerializeField]
    private TrainRail _trainSound;
    protected void Start()
    {
        StartCoroutine(DestroyTrain());

    }
    private IEnumerator DestroyTrain()
    {
        _trainSound.TrainRailvent.Post(gameObject);

        yield return new WaitForSeconds(3f);
        _trainSound.TrainRailvent.Stop(gameObject);

        Destroy(gameObject);

    }
}
