using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [SerializeField]
    private TMP_Text scoreDisplay;

    [SerializeField]
    private int scores;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void OnGUI()
    {
        scoreDisplay.text = scores.ToString();
    }

    public void addScore(int score)
    {
        scores += score;

    }
    public void minusScore(int score)
    {
        if (scores > score)
        {
            scores -= score;
        }
        else
        {
            scores = 0;
        }
    }

    public void Reset()
    {
        scores = 0;
    }
}
