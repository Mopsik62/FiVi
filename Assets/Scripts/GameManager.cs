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

    public void StartInteract(GameObject currentInteractable)
    {
        Debug.Log("Взаимодействие с : " + currentInteractable.name);
        Interactable interactable = currentInteractable.GetComponent<Interactable>();
        switch (interactable.type)
        {
            case InteractableType.Dialogue:
                DialogueWindow.SetActive(true);
                break;
            case InteractableType.Teleport:
                interactable.Teleport();

                break;
        }
    }

}
