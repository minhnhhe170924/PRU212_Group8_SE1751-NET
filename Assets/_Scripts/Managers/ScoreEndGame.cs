using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreEndGame : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = CoinManager.instance.GetScoreEndGamme().ToString();
    }
}
