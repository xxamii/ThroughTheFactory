using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    // State
    private bool _isWalking = false;
    private bool _isJumping = false;
    private bool _isFalling = false;
    private bool _isDead = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Walk()
    {
        if (!_isWalking && !_isDead)
        {
            _animator.SetBool("IsWalking", true);
            _isWalking = true;
        }
    }

    public void Stop()
    {
        if (_isWalking)
        {
            _animator.SetBool("IsWalking", false);
            _isWalking = false;
        }
    }

    public void Jump()
    {
        if (!_isJumping && !_isDead)
        {
            _animator.SetBool("IsJumping", true);
            _animator.SetBool("IsFalling", false);
            _isJumping = true;
        }
    }

    public void Fall()
    {
        if (!_isFalling && !_isDead)
        {
            _animator.SetBool("IsFalling", true);
            _animator.SetBool("IsJumping", false);
            _isJumping = false;
            _isFalling = true;
        }
    }

    public void Land()
    {
        if (_isFalling && !_isDead)
        {
            _animator.SetBool("IsFalling", false);
            _animator.SetBool("IsJumping", false);
            _isFalling = false;
        }
    }

    public void Die()
    {
        _animator.SetTrigger("Die");
        _isDead = true;
    }
}
