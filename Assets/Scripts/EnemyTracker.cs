using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTracker : MonoBehaviour
{
    [SerializeField]
    private int enemyCount;
    private void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Fighter");
        enemyCount = enemies.Length;
    }
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Fighter");

        if (enemies.Length == 0)
        {
            StartCoroutine(LoadWithDelay());
        }
    }

    private IEnumerator LoadWithDelay()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Hub");

    }
}
