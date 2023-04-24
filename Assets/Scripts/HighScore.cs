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
    public void Start()
    {
        highscore = PlayerPrefs.GetFloat("HScore", 0);
    }

    public void EnterHighScore(float score)
    {
        TxScore.text = "Score:\n " + score.ToString("0.00");
        if (score > highscore)
        {
            highscore = score;
            NewHighScore.Invoke();
   
            PlayerPrefs.SetFloat("HScore", score);
            PlayerPrefs.Save();
        }
        TxHigh.text = "High Score:\n " + highscore.ToString("0.00");
    }
}
