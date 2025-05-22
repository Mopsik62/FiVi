using System.Collections;
using UnityEngine;

public class Train : MonoBehaviour
{
    protected void Start()
    {
        StartCoroutine(DestroyTrain());

    }
    private IEnumerator DestroyTrain()
    {

        yield return new WaitForSeconds(10f);
        Destroy(gameObject);

    }
}
