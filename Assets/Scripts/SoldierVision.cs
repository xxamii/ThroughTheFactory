using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierVision : MonoBehaviour
{
    // Dependecies
    [SerializeField] private Transform _visionStartPoint;
    [SerializeField] private LayerMask _enemyMask;
    private Transform _transform;
    private Animator _animator;
    private Player _player;
    private SoldierMovement _movement;
    private LineRenderer _lineRenderer;

    // Values
    private float _visionDistance = 2f;
    private Color _red;

    // State
    private bool _lookingForPlayer = true;

    private void Start()
    {
        _transform = this.transform;
        _animator = GetComponent<Animator>();
        _player = Player.Instance;
        _movement = GetComponent<SoldierMovement>();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.useWorldSpace = true;
        _red = new Color(181f / 255f, 36f / 255f, 82f / 255f);
    }

    private void FixedUpdate()
    {
        if (_lookingForPlayer)
            PlayerCheck();
    }

    private void PlayerCheck()
    {
        RaycastHit2D ray = Physics2D.Raycast(_visionStartPoint.position, _transform.right, _visionDistance, _enemyMask.value);

        if (ray)
        {
            Debug.DrawLine(_visionStartPoint.position, ray.point, Color.red);
            _lineRenderer.SetPosition(0, _visionStartPoint.position);
            _lineRenderer.SetPosition(1, new Vector2(_visionStartPoint.position.x + (ray.point.x - _visionStartPoint.position.x), _visionStartPoint.position.y));
        }
        else
        {
            Debug.DrawRay(_visionStartPoint.position, _transform.right * _visionDistance, Color.red);
            _lineRenderer.SetPosition(0, _visionStartPoint.position);
            _lineRenderer.SetPosition(1, new Vector2(_visionStartPoint.position.x + (_transform.right.x * _visionDistance), _visionStartPoint.position.y));
        }

        if (ray && ray.collider.GetComponent<Player>())
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        _lookingForPlayer = false;
        _lineRenderer.startColor = _red;
        _lineRenderer.endColor = _red;
        _movement.StopWalking();
        _animator.SetTrigger("End");
        _player.EndGame();
    }
}
