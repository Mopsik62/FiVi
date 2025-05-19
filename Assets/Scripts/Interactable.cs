using UnityEngine;

public class Interactable : MonoBehaviour
{
    public InteractableType type;

    [Header("For Teleport")]
    public string NextScene;
    public void Teleport()
    {
        if (GameManager.instance.currentProgress < 2)
        { Debug.Log("� ��� ��� ������!"); }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(NextScene);
        }
    }
}
