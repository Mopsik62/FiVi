using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Fighter
{
    public static Player instance;
    public float moveSpeed = 5f;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    [SerializeField]
    private HealthUI _healthUI;

    [SerializeField]
    private Weapon _curWeaponMele;
    [SerializeField]
    private Weapon _curWeaponRanged;


    [SerializeField]
    private Footsteps_Concrete _footsteps;
    [SerializeField]
    private Footsteps_Gravel _footsteps_gravel;
    [SerializeField]
    private WoodStickHit _woodenStickHit;
    [SerializeField]
    private PC_Damage _damageRecive;
    [SerializeField]
    private float _footsteps_cooldown; //время аниматора для 1 шага
    private float _footsteps_timer;

    public bool _canAttack = false;
    private bool isAttacking = false;
    public bool canMove = true;

    private Vector2 _lastMoveDirection;
    private bool isDashing = false;
    [SerializeField] private float dashDistance = 3f;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashCooldown = 1f;
    private bool canDash = true;

    protected override void Awake()
    {
        base.Awake();

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

        if (moveInput != Vector2.zero)
        {
            _lastMoveDirection = moveInput.normalized;
        }
        else
        {
            _lastMoveDirection = Vector2.zero;
        }

            if (!canMove) return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && (_lastMoveDirection != Vector2.zero))
        {
            Dash();
        }

        if (!isAttacking && !isDashing)
        {
            AnimateMovement();
        }

        if (Input.GetMouseButtonDown(0))
        {
            AttackWithMelee();
        }
        if (Input.GetMouseButtonDown(1))
        {
            AttackWithRanged();
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
            if (moveInput.x != 0)
            {
                if (moveInput.x == 1)
                {
                  animator.Play("Right");
                }
                else
                {
                  animator.Play("Left");
                }
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
    public void AttackWithMelee()
    {
        if (!_canAttack)
            return;
        if (!isAttacking)
        {
            _curWeaponMele.Attack();
            isAttacking = true;
            animator.Play("Attack");
            _woodenStickHit.WoodStickHitEvent.Post(gameObject);
            Invoke(nameof(EndAttack), _curWeaponMele.cooldown);
        }
    }
    public void AttackWithRanged()
    {
        if (!_canAttack)
            return;
        if (!isAttacking)
        {
            _curWeaponRanged.Attack();
            isAttacking = true;
            animator.Play("AttackRanged");
            Invoke(nameof(EndAttack), _curWeaponRanged.cooldown);
        }
    }
    private void EndAttack()
    {
        isAttacking = false;
    }

/*    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.name);
        if (collision.collider.CompareTag("Fighter"))
        {
            Damage dmg = new()
            {
                origin = transform.position,
                damage = 1,
                pushForce = 1
            };
            collision.collider.SendMessage("ReciveDamage", dmg);
        }
    }*/

    public override void ReciveDamage(Damage dmg)
    {
        if (CanTakeDamage)
        {
            _damageRecive.PCDamageEvent.Post(gameObject);
            
        }
        base.ReciveDamage(dmg);
        _healthUI.UpdateHearts(curHp); // каждый раз вызывается неоч
    }

    protected void Dash()
    {
        if (isDashing) return;
        animator.Play("Dash");
        isDashing = true;
        canDash = false;
        StartCoroutine(DashCooldownRoutine());
        canMove = false;

        Vector2 dashTarget = rb.position + _lastMoveDirection * dashDistance;
        StartCoroutine(PerformDash(dashTarget));
    }
    private IEnumerator PerformDash(Vector2 targetPos)
    {
        float elapsed = 0f;
        Vector2 startPos = rb.position;

        while (elapsed < dashDuration)
        {
            rb.MovePosition(Vector2.Lerp(startPos, targetPos, elapsed / dashDuration));
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        rb.MovePosition(targetPos); 
        isDashing = false;
        canMove = true;
    }
    private IEnumerator DashCooldownRoutine()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
