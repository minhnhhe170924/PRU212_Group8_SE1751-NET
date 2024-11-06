using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinnishLevel : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var currentScencePlay = SceneManager.GetActiveScene().name;
            if (currentScencePlay.Equals("LevelEasy"))
            {
                PlayerPrefs.SetString("currentScenceNextLevel", currentScencePlay);
                PlayerPrefs.SetInt("passLevelEasy", 1);
            }
            if (currentScencePlay.Equals("LevelMedium"))
            {
                PlayerPrefs.SetString("currentScenceNextLevel", currentScencePlay);
                PlayerPrefs.SetInt("passLevelMedium", 1);
            }
            SceneManager.LoadScene("WinGame");
        }
    }
}
