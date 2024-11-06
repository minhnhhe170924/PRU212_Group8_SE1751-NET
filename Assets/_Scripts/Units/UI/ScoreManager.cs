using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI textScore;

    public static ScoreManager Instance { get; private set; }
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScore(int points) {
        int currentScore = PlayerPrefs.GetInt("CurrentScore", 0);
        currentScore += points;
        UpdateScoreUI(currentScore);
        PlayerPrefs.SetInt("CurrentScore", currentScore);
    }

    private void UpdateScoreUI(int score) {
        textScore.text = "Score: " + score;
    }
}
