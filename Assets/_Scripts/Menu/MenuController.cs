using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //[SerializeField] GameObject pauseMenuScence;
    public void Play()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelOfMenu");
        PlayerPrefs.SetInt("passLevelEasy", 1);
        //PlayerPrefs.SetInt("passLevelMedium", 0);
        //PlayerPrefs.SetInt("HighScoreEasy", 0);
    }
    public void AboutUs()
    {
        SceneManager.LoadScene("AboutUs");
    }
    public void Replay()
    {
        var currenrsceenReplay =PlayerPrefs.GetString("currentScenceRePlay");
        SceneManager.LoadScene(currenrsceenReplay);
    }
    public void NextLevel()
    {
        var currentScencePlay = PlayerPrefs.GetString("currentScenceNextLevel");
        if (currentScencePlay.Equals("LevelEasy"))
        {
            SceneManager.LoadScene("LevelMedium");
        }
        if (currentScencePlay.Equals("LevelMedium"))
        {
            SceneManager.LoadScene("LevelHard");
        }
        
    }
    public void ExitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");

    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Instruction()
    {
        SceneManager.LoadScene("Instruction");
    }
    public void PlayEasy()
    {
        SceneManager.LoadScene("GameStory");
    }
    public void PlayMedium()
    {
        SceneManager.LoadScene("LevelMedium");
    }
    public void PlayHard()
    {
        SceneManager.LoadScene("LevelHard");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
}
