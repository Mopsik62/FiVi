using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameObject StartDialogueWindow;

    [SerializeField]
    private GameObject MumAndDad;

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
    [SerializeField]
    public PropsUse _foodUse;
    [SerializeField]
    private PropsGet _foodGet;

    public bool HasTicket = false;

    private bool FirstEntry = true;

    [SerializeField]
    private Food _currentFood;
    [SerializeField]
    private Image _foodHolder;

    private DialogueStruct _currentDialogData;
    private int _currentDialogSentence;
    public string CurrentLocationName { get; private set; }

    protected void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }



        NextProgression();
        SceneManager.sceneLoaded += OnSceneLoaded;
        CurrentMoney = int.Parse(_currentMoney.text);
        UpdateMoneyHandler();
        _foodHolder.preserveAspect = true;
        UpdateFood();
    }
    private void Start()
    {

        if (FirstEntry)
        {
            StartCoroutine(Zastavka());
            FirstEntry = false;
        }
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
        Debug.Log("�������������� � : " + currentInteractable.name);
        Interactable interactable = currentInteractable.GetComponent<Interactable>();
        switch (interactable.type)
        {
            case InteractableType.Shop:
                Debug.Log("Shop");
                interactable.OpenShop();
                PlayerMove(false);

                break;

            case InteractableType.Dialogue:
                _currentDialogData = currentInteractable.GetComponent<DialogueStruct>();
                HasTicket = true;
                StartDialogue();
                break;
            case InteractableType.Teleport:
                if (!HasTicket)
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
        PlayerMove(false);
        _currentDialogSentence = 0;
        _dialogueText.text = _currentDialogData.lines[_currentDialogSentence];
    }
    public void EndDialogue()
    {
        DialogueWindow.SetActive(false);
        PlayerMove(true);
        _currentDialogSentence = 0;
        NextProgression();
    }

    public void NextProgression()
    {

        if (currentProgress + 1 < texts.Count)
        {
            currentProgress++;
            _playerQuestProgression.text = texts[currentProgress];
        }
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
        Debug.Log("������� �������� ��: " + CurrentLocationName);
    }
    public void ResetSound()
    {
       // _hub_music.HubMusiclEvent.Stop(gameObject);
       // _roomTone.RoomToneInStationlEvent.Stop(gameObject);
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

    public void PlayerMove(bool canMove)
    {
        Player.instance.canMove = canMove;
    }
    public void BuyFood(Food food)
    {
        Debug.Log(CurrentMoney);
        if (CurrentMoney >= food.Price)
        {
            LoseMoney(food.Price);
            AddFood(food);
        }
    }
    public void AddFood(Food food)
    {
        if (_currentFood != null)
            Destroy(_currentFood.gameObject);
        _foodGet.PropsGetEvent.Post(gameObject);
        Food newFood = Instantiate(food);
        newFood.gameObject.SetActive(false);
        DontDestroyOnLoad(newFood.gameObject);

        _currentFood = newFood;
        UpdateFood();
        Debug.Log("��������� ���!");
    }

    public void ConsumeFood( )
    {
        Debug.Log("EAT FOOD");
        if (_currentFood == null)
            return;
        _foodUse.PropsUSeEvent.Post(gameObject);
        _currentFood.Consume();
        Destroy(_currentFood.gameObject);
        _currentFood = null;
        UpdateFood();
    }
    public void UpdateFood()
    {
        if (_currentFood != null)
        {
            Color color = _foodHolder.color;
            color.a = 1f;
            _foodHolder.color = color;
            _foodHolder.sprite = _currentFood.image.sprite;
        }
        else
        {
            Color color = _foodHolder.color;
            color.a = 0f;
            _foodHolder.color = color;
        }
    }
    private IEnumerator Zastavka()
    {
        Player.instance.canMove = false;
        StartDialogueWindow.SetActive(true);
        MumAndDad.SetActive(true);
        yield return new WaitForSeconds(4f);


    }
    public void MakePlayerMove()
    {
        Player.instance.canMove = true;
        MumAndDad.SetActive(false);

        StartDialogueWindow.SetActive(false);
    }

}
