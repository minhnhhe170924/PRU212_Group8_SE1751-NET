using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathWithPoison : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("DeathWithPoison");
        if (collision.gameObject.CompareTag("Player"))
        {
            var currentScencePlay = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("currentScenceRePlay", currentScencePlay);
            SceneManager.LoadScene("GameOver");
        }
    }
}
