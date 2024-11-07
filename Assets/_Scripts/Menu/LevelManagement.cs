using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagement : MonoBehaviour
{
    public static LevelManagement instance;
    [SerializeField] public Button btnMediumActive;
    [SerializeField] public Button btnMediumInActive;
    [SerializeField] public Button btnHardActive;
    [SerializeField] public Button btnHardInActive;
    public bool passLevelEasy = false;
    public bool passlevelMedium = false;
    // Start is called before the first frame update
    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        UpdateButtonStates();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateButtonStates();
    }
    void UpdateButtonStates()
    {

        // Retrieve level completion status from PlayerPrefs
        if (PlayerPrefs.GetInt("passLevelEasy", 0) == 1)
        {
            passLevelEasy = true;
        }
        if (PlayerPrefs.GetInt("passLevelMedium", 0) == 1)
        {
            passlevelMedium = true;
        }
        Debug.Log("     " + passLevelEasy + " " + passlevelMedium);
        // Logic to control button states based on level completion
        if (!passLevelEasy) // Before passing Easy level
        {
            SetButtonState(btnMediumActive, false);
            SetButtonState(btnMediumInActive, true);
            btnMediumInActive.enabled = false;
            SetButtonState(btnHardActive, false);
            SetButtonState(btnHardInActive, true);
            btnHardInActive.enabled = false;

        }
        else if (passLevelEasy && !passlevelMedium) // After passing Easy, before passing Medium
        {
            SetButtonState(btnMediumActive, true);
            SetButtonState(btnMediumInActive, false);
            SetButtonState(btnHardActive, false);
            SetButtonState(btnHardInActive, true);
            btnHardInActive.enabled = false;
        }
        else if (passlevelMedium) // After passing Medium level
        {
            SetButtonState(btnMediumActive, true);
            SetButtonState(btnMediumInActive, false);
            SetButtonState(btnHardActive, true);
            SetButtonState(btnHardInActive, false);
        }
    }

    // Helper method to enable or disable a button
    void SetButtonState(Button button, bool isActive)
    {
        button.gameObject.SetActive(isActive); // Enables or disables the button's GameObject
    }
}
