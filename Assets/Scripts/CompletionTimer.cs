using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletionTimer : MonoBehaviour
{
    [SerializeField] private Text _timeText;

    private static float _time;
    private float _elapsed;

    private void Awake()
    {
        if (_timeText)
        {
            _elapsed = Time.time - _time;
            _timeText.text = SecondsToTime(_elapsed);
        }
        else
        {
            _time = Time.time;
        }
    }

    private string SecondsToTime(float seconds)
    {
        int s = (int)seconds;
        int m = 0;
        int h = 0;

        while (s - 60 > 0)
        {
            s -= 60;
            m += 1;
        }

        while (m - 60 > 0)
        {
            m -= 60;
            h += 1;
        }

        return h.ToString().PadLeft(2, '0') + ':' + m.ToString().PadLeft(2, '0') + ':' + s.ToString().PadLeft(2, '0');
    }
}
