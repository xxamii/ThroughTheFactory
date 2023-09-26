using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Dependencies
    private Rigidbody2D _rigidBody;
    private Transform _transform;
    [SerializeField] private Transform _leftGroundCheck, _rightGroundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _wallCheck;
    [SerializeField] private ParticleSystem _dustParticles;
    private PlayerAnimator _animator;
    private Player _player;
    private PlayerSounds _audio;

    // Values
    private float _moveSpeed = 6f;
    private float _wallJumpX = 6f;
    private float _jumpForce = 12f;
    private float _wallJumpForce = 9f;
    private float _groundCheckDistance = 0.6f;
    private float _wallCheckDistance = 0.48f;
    private float _wallSlideForce = 0f;

    // State
    private int _inputDirection;
    private int _lookingDirection;
    private bool _jumpInput = false;
    private bool _wallJumpAvailable = false;
    private bool _isLookingRight = true;
    private bool _canMove = true;
    private bool _atWall = false;

    // Delayed Jump
    private float _delayedJumpTime = 0.1f;
    private float _delayedJumpTimer;

    // Coyote Time
    private int _coyoteTimeFrames = 8;
    private int _coyoteTimer = 0;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<PlayerAnimator>();
        _audio = GetComponent<PlayerSounds>();
        _transform = this.transform;
        _player = GetComponent<Player>();
        _isLookingRight = _transform.rotation.eulerAngles.y < 90;
    }

    // Update is called once per frame
    private void Update()
    {
        // Moving input
        if (_canMove)
        {
            _inputDirection = (int)Input.GetAxisRaw("Horizontal");

            if (_inputDirection != 0 && !_atWall)
            {
                _animator.Walk();
                _lookingDirection = _inputDirection;
            }
            else
            {
                _animator.Stop();
            }

            if (Input.GetButtonDown("Jump"))
            {
                _delayedJumpTimer = _delayedJumpTime + Time.time;
                _jumpInput = true;
            }
        }

        // Set coyote timer
        if (IsGrounded())
        {
            _coyoteTimer = 0;
        }
        else
        {
            _coyoteTimer++;
        }

        // Set fall animation
        if (IsGrounded())
        {
            _animator.Land();
        }
        else if (!IsGrounded() && _rigidBody.velocity.y < 0f)
        {
            _animator.Fall();
        }

        // Fall game over
        if (_transform.position.y < -6f)
        {
            _player.EndGame();
        }
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            MoveHorizontally();
            Jump();
        }
        WallCheck();
    }

    private void MoveHorizontally()
    {
        if (_isLookingRight && _inputDirection < 0 || !_isLookingRight && _inputDirection > 0)
        {
            Flip();
        }

        _rigidBody.velocity = new Vector2(_inputDirection * _moveSpeed, _rigidBody.velocity.y);
    }

    private void Jump()
    {
        if (!_wallJumpAvailable && !(_inputDirection != 0 && _atWall))
            _jumpInput = false;

        if (_coyoteTimer < _coyoteTimeFrames && _delayedJumpTimer > Time.time)
        {
            _animator.Jump();
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpForce);
            PlayDust();
            _audio.PlayTakeof();
            _jumpInput = false;
            _coyoteTimer = _coyoteTimeFrames;
        }
        else if (((_inputDirection != 0 && _atWall) || _wallJumpAvailable) && _jumpInput)
        {
            _rigidBody.velocity = new Vector2(-_lookingDirection * _wallJumpX, _wallJumpForce);
            PlayDust();

            if ((_inputDirection != 0 && _atWall) && !_wallJumpAvailable)
                _audio.PlayWallGrab();

            _audio.PlayWallJump();
            _jumpInput = false;

            StartCoroutine(StopMoveRoutine());
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D leftRay = Physics2D.Raycast(_leftGroundCheck.position, Vector2.down, _groundCheckDistance, _groundLayer.value);
        RaycastHit2D rightRay = Physics2D.Raycast(_rightGroundCheck.position, Vector2.down, _groundCheckDistance, _groundLayer.value);

        return leftRay || rightRay;
    }

    private void WallCheck()
    {
        RaycastHit2D ray = Physics2D.Raycast(_wallCheck.position, _transform.right, _wallCheckDistance, _groundLayer.value);

        _atWall = ray;

        if (!IsGrounded() && _inputDirection != 0 && _atWall && !_wallJumpAvailable && _rigidBody.velocity.y <= 0)
        {
            _wallJumpAvailable = true;
            _audio.PlayWallGrab();
        }
        else if ((!_atWall || IsGrounded()) && _wallJumpAvailable)
        {
            _wallJumpAvailable = false;
        }

        if (_wallJumpAvailable)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, Mathf.Clamp(_rigidBody.velocity.y, -_wallSlideForce, float.MaxValue));
        }
    }

    private void Flip()
    {
        _transform.Rotate(0, 180, 0);
        _isLookingRight = !_isLookingRight;
    }

    private IEnumerator StopMoveRoutine()
    {
        _canMove = false;

        yield return new WaitForSeconds(0.1f);

        _canMove = true;
    }

    public void StopInput()
    {
        _canMove = false;
    }

    public void StopMove()
    {
        _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);
        _animator.Stop();
    }

    public void PlayDust()
    {
        _dustParticles.Play();
    }
}
