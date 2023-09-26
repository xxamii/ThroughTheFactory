using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMovement : MonoBehaviour
{
    // Dependecies
    private Rigidbody2D _rigidBody;
    private Transform _transform;
    [SerializeField] private Transform _groundCheck, _wallCheck;
    [SerializeField] private LayerMask _groundLayer;

    // Values
    private float _walkSpeed = 3f;
    private float _groundCheckDistance = 0.2f;
    private float _wallCheckDistance = 0.4f;

    //State
    private bool _isLookingRight = true;
    private bool _isWalking = false;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _transform = this.transform;

        _isLookingRight = _transform.rotation.eulerAngles.y < 90;
    }

    private void FixedUpdate()
    {
        if (_isWalking)
            FlipCheck();
    }

    private void FlipCheck()
    {
        RaycastHit2D groundRay = Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundCheckDistance, _groundLayer.value);
        RaycastHit2D wallRay = Physics2D.Raycast(_wallCheck.position, _transform.right, _wallCheckDistance, _groundLayer.value);

        if (!groundRay || wallRay)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _transform.Rotate(0, 180, 0);
        _isLookingRight = !_isLookingRight;
        ResetVelocity();
    }

    public void StartWalking()
    {
        _rigidBody.velocity = _transform.right * _walkSpeed;
        _isWalking = true;
    }

    public void StopWalking()
    {
        _rigidBody.velocity = Vector2.zero;
        _isWalking = false;
    }

    private void ResetVelocity()
    {
        _rigidBody.velocity = _transform.right * _walkSpeed;
    }
}
