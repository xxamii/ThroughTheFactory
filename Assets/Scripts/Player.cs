using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Dependecies
    private PlayerAnimator _animator;
    private PlayerMovement _movement;
    private Collider2D _collider;
    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidBody;
    private PlayerSounds _audio;
    [SerializeField] private ParticleSystem _deathParticles;

    // Singleton
    public static Player Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<PlayerAnimator>();
        _movement = GetComponent<PlayerMovement>();
        _collider = GetComponent<BoxCollider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _audio = GetComponent<PlayerSounds>();
    }

    public void EndGame()
    {
        _animator.Stop();
        _animator.Land();
        _movement.StopInput();
        _movement.StopMove();
        StartCoroutine(RestartRoutine());
    }

    public void Die()
    {
        _movement.StopInput();
        _movement.StopMove();
        _rigidBody.bodyType = RigidbodyType2D.Kinematic;
        _rigidBody.velocity = Vector2.zero;
        _collider.enabled = false;
        _animator.Die();
        _audio.PlayHurt();
        EndGame();
    }

    public void Blood()
    {
        _renderer.enabled = false;
        _deathParticles.Play();
        _audio.PlayDie();
    }

    private IEnumerator RestartRoutine()
    {
        yield return new WaitForSeconds(1.5f);

        SceneLoader.Restart();
    }
}
