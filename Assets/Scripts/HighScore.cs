using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HighScore : MonoBehaviour
{
    public float highscore;
    public UnityEvent NewHighScore;

    // Start is called before the first frame update
    public void EnterHighScore(float score)
    {
        if (score > highscore)
        {
            highscore = score;
            NewHighScore.Invoke();
        }
    }
}
