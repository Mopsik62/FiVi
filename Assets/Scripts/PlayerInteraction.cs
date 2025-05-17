using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    private GameObject currentInteractable = null;

    void Update()
    {
        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.instance.StartInteract(currentInteractable);          
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interractable"))
        {
            currentInteractable = other.gameObject;
            Debug.Log("Вошёл в зону: " + currentInteractable.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interractable"))
        {
            if (other.gameObject == currentInteractable)
            {
                Debug.Log("Покинул зону: " + currentInteractable.name);
                currentInteractable = null;
            }
        }
    }
}
