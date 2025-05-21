using UnityEngine;

public class Interactable : MonoBehaviour
{
    public InteractableType type;

    [Header("For Teleport")]
    public string NextScene;

    [Header("For Shop")]
    public GameObject Shop;
    public void Teleport()
    {
        if (GameManager.instance.currentProgress < 2)
        { Debug.Log("� ��� ��� ������!"); }
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
    }
}
