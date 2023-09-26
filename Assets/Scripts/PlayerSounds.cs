using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _stepSound;
    [SerializeField] private AudioClip _takeofSound;
    [SerializeField] private AudioClip _landSound;
    [SerializeField] private AudioClip _wallGrabSound;
    [SerializeField] private AudioClip _hurtSound;
    [SerializeField] private AudioClip _popSound;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayStep()
    {
        Play(_stepSound, 0.2f);
    }

    public void PlayTakeof()
    {
        Play(_takeofSound, 0.1f);
    }

    public void PlayLand()
    {
        Play(_landSound, 0.2f);
    }

    public void PlayWallGrab()
    {
        Play(_wallGrabSound, 0.25f);
    }

    public void PlayWallJump()
    {
        Play(_takeofSound, 0.1f);
    }

    public void PlayHurt()
    {
        Play(_hurtSound, 0.2f);
    }

    public void PlayDie()
    {
        Play(_popSound, 0.1f);
    }

    private void Play(AudioClip clip, float sound = 1f)
    {
        _audioSource.PlayOneShot(clip, sound);
    }
}
