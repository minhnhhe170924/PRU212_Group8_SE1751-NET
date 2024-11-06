using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour {
    public GameObject PausePanelObject;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void Pause() {
        PausePanelObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue() {
        PausePanelObject.SetActive(false); 
        Time.timeScale = 1;
    }

    public void ExitToMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
