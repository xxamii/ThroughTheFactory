using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOnClick : MonoBehaviour
{
    [SerializeField] private int _sceneToLoadId;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneLoader.LoadScene(_sceneToLoadId);
        }
    }
}
