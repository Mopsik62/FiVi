using System.Collections;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public InteractableType type;

    [Header("For Teleport")]
    public string NextScene;

    [Header("For Shop")]
    public GameObject Shop;


    void Awake()
    {

        StartCoroutine(SetupScene());
    }
    IEnumerator SetupScene( )
    {
        yield return null;
    }
        public void Teleport()
    {
        if (GameManager.instance.currentProgress < 2)
        { Debug.Log("У вас нет билета!"); }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(NextScene);
        }
    }

    public void OpenShop()
    {
        Shop.SetActive(true);
    }

    public void CloseShop()
    {
        Shop.SetActive(false);
        GameManager.instance.PlayerMove(true);
    }
}
