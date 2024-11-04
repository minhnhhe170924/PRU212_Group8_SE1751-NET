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
        SceneManager.LoadScene(1);
        //PlayerPrefs.SetInt("passLevelEasy", 0);
        //PlayerPrefs.SetInt("HighScoreEasy", 0);
    }
    public void AboutUs()
    {
        SceneManager.LoadScene("AboutUs");
    }
    public void Replay()
    {
        //SceneManager.LoadScene(PlayerController.instance.currentScencePlay);
        PlayerPrefs.SetFloat("totalHeartsAfterdie", 3);
    }
    public void NextLevel()
    {
        //SceneManager.LoadScene(PlayerController.instance.currentScencePlay+1);
        PlayerPrefs.SetFloat("totalHeartsAfterdie", 3);
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
        SceneManager.LoadScene(8);
    }
    //public void PauseGame()
    //{
    //    Time.timeScale = 0;
    //    pauseMenuScence.SetActive(true);
    //}
    //public void Resume()
    //{
    //    Time.timeScale = 1;
    //    pauseMenuScence.SetActive(false);
    //}
}
