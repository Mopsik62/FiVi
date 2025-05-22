using System.Collections;
using UnityEngine;

public class Wisp : Enemy
{
    [SerializeField]
    private float _dashDistance = 5f;
    private bool _isDashing = false;
    protected override void Awake()
    {
        base.Awake();
        _lastSpecial = Time.time;
    }
    protected override void FixedUpdate()
    {
        if (_isDashing)
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }
        base.FixedUpdate();
        float distance = Vector2.Distance(transform.position, playerPosition.position);
        if ((distance < _dashDistance) && (Time.time - _lastSpecial > _specialCooldown))
        {
            Debug.Log("Start Dashing");
            _lastSpecial = Time.time;
            StartCoroutine(SpecialAttack());
            }

    }


    private IEnumerator SpecialAttack()
    {
        _isDashing = true;
        _rb.linearVelocity = Vector2.zero;
        anim.SetBool("ChargeSpecial",true);
        yield return new WaitForSeconds(2f);
        anim.SetBool("ChargeSpecial", false);
        anim.SetBool("Special", true);
        StartCoroutine(Dash());

    }

    private IEnumerator Dash()
    {

        float dashDuration = 0.75f;
        float dashTimer = 0f;
        float dashSpeed = 10f;

        Vector2 direction = ((Vector2)(playerPosition.position - transform.position)).normalized;


        while (dashTimer < dashDuration)
        {
            Vector2 pixelPosition = new Vector2(
            Mathf.Round((_rb.position.x + direction.x * moveSpeed * dashSpeed * Time.fixedDeltaTime) * 54) / 54, //округление по нашему PPU иначе дергается
            Mathf.Round((_rb.position.y + direction.y * moveSpeed * dashSpeed * Time.fixedDeltaTime) * 54) / 54
            );
            _rb.MovePosition(pixelPosition);
            dashTimer += Time.deltaTime;
            yield return null;
        }

        _isDashing = false;
        anim.SetBool("Special", false);

    }

}
