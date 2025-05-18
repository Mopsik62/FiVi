using UnityEngine;

public class Interactable : MonoBehaviour
{
    public InteractableType type;

    [Header("For Teleport")]
    public string NextScene;
    public void Teleport()
    {
      UnityEngine.SceneManagement.SceneManager.LoadScene(NextScene);
    }
}
