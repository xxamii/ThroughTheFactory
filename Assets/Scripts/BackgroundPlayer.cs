using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPlayer : MonoBehaviour
{
    private static BackgroundPlayer _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
