using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVlumne : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("log" + PlayerPrefs.GetFloat("musicVolumne").ToString());
        AudioListener.volume = PlayerPrefs.GetFloat("musicVolumne");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
