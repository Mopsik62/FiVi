using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private TextMeshProUGUI _currentMoney;

    [SerializeField]
    public int CurrentMoney { get; private set; }

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

    [SerializeField]
    public Fight_Music _fight_music;
    [SerializeField]
    public RoomTone_InStation _roomTone;
    [SerializeField]
    public Hub_Music _hub_music;

    private DialogueStruct _currentDialogData;
    private int _currentDialogSentence;
    public string CurrentLocationName { get; private set; }

    protected void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        NextProgression();

        SceneManager.sceneLoaded += OnSceneLoaded;
        CurrentMoney = int.Parse(_currentMoney.text);
        UpdateMoneyHandler();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetLocationName(scene.name);
        Debug.Log("GAMEMANAGER LOAD SCENE");
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
                if (currentProgress < 2)
                    return;
                Debug.Log(currentProgress);
                interactable.Teleport();
                NextProgression();
                SetLocationName("Level 1");
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

    public void SetLocationName(string newLocation)
    {

        CurrentLocationName = newLocation;
        switch (CurrentLocationName)
        {
            case "Hub":
                ResetSound();
                _hub_music.HubMusiclEvent.Post(gameObject);
                _roomTone.RoomToneInStationlEvent.Post(gameObject);
                Player.instance._canAttack = false;
                break;
            case "Level 1":
                ResetSound();
                Player.instance._canAttack = true;
                _fight_music.FightMusiclEvent.Post(gameObject);
                break;
        }
        Debug.Log("Локация изменена на: " + CurrentLocationName);
    }
    public void ResetSound()
    {
        _hub_music.HubMusiclEvent.Stop(gameObject);
        _roomTone.RoomToneInStationlEvent.Stop(gameObject);
        _fight_music.FightMusiclEvent.Stop(gameObject);

    }

    public void GetMoney(int money)
    {
        CurrentMoney += money;
        UpdateMoneyHandler();
    }
    public void LoseMoney(int money)
    {
        CurrentMoney -= money;
        UpdateMoneyHandler();
    }
    public void UpdateMoneyHandler()
    {
        _currentMoney.text = CurrentMoney.ToString();
    }    


}
