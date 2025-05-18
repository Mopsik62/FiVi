using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private TextMeshProUGUI _playerQuestProgression;
    [SerializeField]
    private GameObject DialogueWindow;
    [SerializeField]
    private TextMeshProUGUI _dialogueText;
    [SerializeField] 
    private List<string> texts = new List<string>();
    [SerializeField]
    public int currentProgress = 0;
    private DialogueStruct _currentDialogData;
    private int _currentDialogSentence;

    protected void Awake()
    {
        instance = this;
        NextProgression();
    }

    public void StartInteract(GameObject currentInteractable)
    {
        Debug.Log("Взаимодействие с : " + currentInteractable.name);
        Interactable interactable = currentInteractable.GetComponent<Interactable>();
        switch (interactable.type)
        {
            case InteractableType.Dialogue:
                _currentDialogData = currentInteractable.GetComponent<DialogueStruct>();
                StartDialogue();
                break;
            case InteractableType.Teleport:
                interactable.Teleport();
                NextProgression();
                break;
        }
    }

    public void StartDialogue()
    {
        DialogueWindow.SetActive(true);
        Player.instance.canMove = false;
        _currentDialogSentence = 0;
        _dialogueText.text = _currentDialogData.lines[_currentDialogSentence];
    }
    public void EndDialogue()
    {
        DialogueWindow.SetActive(false);
        Player.instance.canMove = true;
        _currentDialogSentence = 0;
        NextProgression();
    }

    public void NextProgression()
    {

        currentProgress = currentProgress + 1;
        _playerQuestProgression.text = texts[currentProgress];
    }
    public void NextDialogueLine()
    {
        Debug.Log("next dialogue line");
        _currentDialogSentence++;
        if (_currentDialogSentence < _currentDialogData.lines.Count)
        {
         _dialogueText.text = _currentDialogData.lines[_currentDialogSentence];
        }
        else
        {
            EndDialogue();
        }
    }

}
