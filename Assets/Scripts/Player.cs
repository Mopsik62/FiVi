using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 5f;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (moveInput != Vector2.zero)
        {
            transform.localScale = new Vector3(5, 5, 5);
            if (moveInput.x != 0)
            {
                if (moveInput.x == 1)
                { transform.localScale = new Vector3(-5, 5, 5); }
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

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }


}
