using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private GameObject DialogueWindow;
    protected void Awake()
    {
        instance = this;
    }

    public void StartDialogue(GameObject currentInteractable)
    {
        Debug.Log("Взаимодействие с : " + currentInteractable.name);
        DialogueWindow.SetActive(true);
    }

}
