using Cinemachine;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // movement
    
    [SerializeField] public float DefaultMoveSpeed = 10f;
    private float _moveSpeed; 
    private Vector2 _moveDirection;
    private bool _isMoving;
    private bool _isRooted = false;

    // jumping
    [SerializeField] public float DefaultJumpForce = 10f;
    private float _jumpForce;
    private bool _isJumping;
    private int _jumpCounter;

    [SerializeField] private float _coyoteTime = 0.2f;
    private float _coyoteTimeCounter;

    [SerializeField] private float _jumpBufferTime = 0.2f;
    private float _jumpBufferTimeCounter;

    // attack
    private bool _isAttacking;
    public bool _isDashing;
    public bool IsAreaAttacking; //temp

    // others
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Vector2 _additionalVelocity;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _moveSpeed = DefaultMoveSpeed;
        _jumpForce = DefaultJumpForce;
        _isJumping = true;
        _jumpCounter = 0;
        _isMoving = false;
    }

    private void Update()
    {
        _animator.SetBool("IsMoving", _isMoving && !_isRooted);
        if (_isJumping)
        {
            _coyoteTimeCounter -= Time.deltaTime;
            _jumpBufferTimeCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        // disable extra movement if rooted or dashing or attacking
        if (_isRooted || _isDashing || IsAreaAttacking/*|| _isAttacking*/) return;
                
        _rigidbody2D.velocity = new Vector2(_moveDirection.x * _moveSpeed, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity += _additionalVelocity;
    }

    ///<summary>Set move speed to default speed * percentage. If isSlowerThanNow is true, update speed only if 
    /// the current speed is slower than the new speed. If onlySpeed is set to false, update both jump force and move speed.
    ///</summary>
    // 디버프 슬로우는 중첩 X, 이속버프 + 슬로우는 중첩 O.
    public void ChangeSpeedByPercentage(float percentage, bool isSlowerThanNow = false, bool onlySpeed = false)
    {
        float newSpeed = DefaultMoveSpeed * percentage;

        // if has buff
        if (_moveSpeed > DefaultMoveSpeed)
        {
            newSpeed = _moveSpeed * percentage;
        }
            
        if (!isSlowerThanNow || _moveSpeed > newSpeed)
        {
            _moveSpeed = newSpeed;
            if (!onlySpeed) _jumpForce = DefaultJumpForce * percentage;
        }
    }

    public void SetMoveSpeedForDuration(float percentage, float duration)
    {
        ChangeSpeedByPercentage(percentage, false, true);
        StartCoroutine(ResetSpeedAfterDuration(duration));
    }

    private IEnumerator ResetSpeedAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        ResetMoveSpeed();
        FindObjectOfType<PlayerCombat>().SetActiveSlow();
    }

    public void ResetMoveSpeed()
    {
        _moveSpeed = DefaultMoveSpeed;
        _jumpForce = DefaultJumpForce;
    }

    public void SetMoveDirection(Vector2 value)
    {
        _moveDirection = value;
        if (value.x == 0 || _isAttacking || _isDashing)
        {
            _isMoving = false;
        }
        else
        {
            // Note: Player sprite default direction is left
            _isMoving = true;
            transform.localScale = new Vector2(-Mathf.Sign(value.x), transform.localScale.y);
        }
    }

    public void EnableMovement(bool strict)
    {
        if (strict)
        {
            _isRooted = false;
        }
        else
        {
            _isAttacking = false;
            if (_moveDirection.x != 0)
            {
                // Note: Player sprite default direction is left
                _isMoving = true;
                transform.localScale = new Vector2(-Mathf.Sign(_moveDirection.x), transform.localScale.y);
            }
        }
    }

    public void DisableMovement(bool strict)
    {
        if (strict)
        {
            _isRooted = true;
            _rigidbody2D.velocity = Vector2.zero;
        }
        else
        {
            // Only allow up, down movement during attack
            //_rigidbody2D.velocity = new Vector2(0.0f, _rigidbody2D.velocity.y);
            _isAttacking = true;
            _isMoving = false;
        }
    }

    public void SetDash()
    {
        _isDashing = true;
        _isMoving = false;
    }

    public void SetJump(bool value)
    {
        if (_jumpCounter > 1) _jumpBufferTimeCounter = _jumpBufferTime;

        if (_isRooted || _isAttacking) return;
        if (_coyoteTimeCounter < 0f && _jumpCounter >= 2) return;
        if (!value) return;

        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
        _jumpCounter += 1;
        _jumpBufferTimeCounter = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            _isJumping = false;
            _jumpCounter = 0;
            _coyoteTimeCounter = _coyoteTime;
            _animator.SetBool("IsJumping", _isJumping);
            if (_jumpBufferTimeCounter > 0f) SetJump(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            _isJumping = true;
            _animator.SetBool("IsJumping", _isJumping);
        }
    }

    public void RemoveDebuffs()
    {
        if (_moveSpeed <= DefaultMoveSpeed) _moveSpeed = DefaultMoveSpeed;
        if (_jumpForce <= DefaultJumpForce) _jumpForce = DefaultJumpForce;
        _isRooted = false;
    }

    // Move vertically to the direction the character is facing
    public void AnimEvent_StartMoveVertical(float xVelocity)
    {
        if (_isRooted) return;

        // Cast a ray to check a wall
        Vector2 playerCentre = new Vector2(transform.position.x, transform.position.y + 0.1f);
        Vector2 faceDir = new Vector2(-Mathf.Sign(gameObject.transform.localScale.x), 0f);
        RaycastHit2D hit = Physics2D.Raycast(playerCentre, faceDir, 1.0f);
        Debug.DrawRay(playerCentre, faceDir, Color.white, 2.0f);
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            Debug.Log("Hit wall");
            return;
        }

        // If no wall in front, move forward
        float dir = -Mathf.Sign(gameObject.transform.localScale.x);
        _additionalVelocity = new Vector2(_additionalVelocity.x + dir * xVelocity, _additionalVelocity.y);
    }

    public void AnimEvent_StartMoveHorizontal(float yVelocity)
    {
        _additionalVelocity = new Vector2(_additionalVelocity.x, yVelocity);
    }

    public void AnimEvent_StopMoveVertical()
    {
        _additionalVelocity = new Vector2(0, _additionalVelocity.y);
    }

    public void AnimEvent_StopMoveHorizontal()
    {
        _additionalVelocity = new Vector2(_additionalVelocity.x, 0);
    }

    public void MoveForSeconds(Vector2 velocity, float seconds)
    {
        StartCoroutine(MoveCoroutine(velocity, seconds));
    }

    private IEnumerator MoveCoroutine(Vector2 velocity, float seconds)
    {
        _rigidbody2D.velocity += velocity;
        yield return new WaitForSeconds(seconds);
        _rigidbody2D.velocity -= velocity;
    }
}
