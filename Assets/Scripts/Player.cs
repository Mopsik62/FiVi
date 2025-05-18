using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;
    public float moveSpeed = 5f;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    [SerializeField]
    private Footsteps_Concrete _footsteps;
    [SerializeField]
    private Footsteps_Gravel _footsteps_gravel;
    [SerializeField]
    private WoodStickHit _woodenStickHit;
    [SerializeField]
    private float _footsteps_cooldown; //время аниматора для 1 шага
    private float _footsteps_timer;
    private bool isAttacking = false;
    public bool canMove = true;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject playerSpawn = GameObject.Find("Player Spawn");
        transform.position = playerSpawn.transform.position;
    }

    void Start()
    {
        _footsteps_timer = 0f;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        if (!canMove) return;
        if (!isAttacking)
        {
            AnimateMovement();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        if (!canMove) return;
        Move();
    }

    void Move()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        if (moveInput != Vector2.zero)
        {
            _footsteps_timer += Time.fixedDeltaTime;
           //Debug.Log(_footsteps_timer);
            if (_footsteps_timer >= _footsteps_cooldown)
            {
                switch (GameManager.instance.CurrentLocationName)
                {
                    case "Hub":
                        _footsteps.footstepConcreteEvent.Post(gameObject);
                        break;
                    case "Level 1":
                        _footsteps_gravel.footstepGravelEvent.Post(gameObject);
                        break;
                }
                _footsteps_timer = 0;
                //Debug.Log("dsad");
            }
        }
    }
    void AnimateMovement()
    {
        if (moveInput != Vector2.zero)
        {
            transform.localScale = new Vector3(2, 2, 2);
            if (moveInput.x != 0)
            {
                if (moveInput.x == 1)
                { transform.localScale = new Vector3(-2, 2, 2); }
                animator.Play("Left");
            }
            else if (moveInput.y == 1)
            {
                animator.Play("Up");
            }
            else
            {
                animator.Play("Down");
            }

        }
        else
        {
            animator.Play("Idle");
        }
    }
    public void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.Play("Attack");
            _woodenStickHit.WoodStickHitEvent.Post(gameObject);
            Invoke(nameof(EndAttack), 0.2f);
        }
    }

    private void EndAttack()
    {
        isAttacking = false;
    }


}
