using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 5f;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    [SerializeField]
    private Footsteps_Concrete _footsteps;
    [SerializeField]
    private float _footsteps_cooldown = 0.9f; //время аниматора для 1 шага
    private float _footsteps_timer;

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
        AnimateMovement();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        if (moveInput != Vector2.zero)
        {
            _footsteps_timer += Time.fixedDeltaTime;
            Debug.Log(_footsteps_timer);
            if (_footsteps_timer >= _footsteps_cooldown)
            {
                _footsteps.footstepConcreteEvent.Post(gameObject);
                _footsteps_timer = 0;
                Debug.Log("dsad");
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



}
