using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    private GameObject currentInteractable = null;
    [SerializeField]
    private GameObject _AnimationE;

    void Update()
    {
        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.instance.StartInteract(currentInteractable);          
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interractable") )
        {
            currentInteractable = other.gameObject;
            Debug.Log("Вошёл в зону: " + currentInteractable.name);
            _AnimationE.SetActive(true);
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
                _AnimationE.SetActive(false);
            }
        }
    }
}
