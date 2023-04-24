using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public float highscore;
    public UnityEvent NewHighScore;
    public Text TxScore, TxHigh; 

    // Start is called before the first frame update
    public void EnterHighScore(float score)
    {
        TxScore.text = "Score:\n " + score;
        if (score > highscore)
        {
            highscore = score;
            NewHighScore.Invoke();
            TxHigh.text = "High Score:\n " + score;
        }
    }
}
