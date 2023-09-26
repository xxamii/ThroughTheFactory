using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitioner : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    private Animator _canvasAnimator;

    private void Start()
    {
        _canvasAnimator = _canvas.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            _canvasAnimator.SetTrigger("FadeOut");
            var player = collision.GetComponent<PlayerMovement>();
            player.StopInput();
            player.StopMove();
            StartCoroutine(TransitionRoutine());
        }
    }

    private IEnumerator TransitionRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        SceneLoader.LoadNextScene();
    }
}
