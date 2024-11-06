using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumneSettings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolumne"))
        {
            PlayerPrefs.SetFloat("musicVolumne", 1);
            load();
        }
        else
        {
            load();
        }
        if (!PlayerPrefs.HasKey("SFXVolumne"))
        {
            PlayerPrefs.SetFloat("SFXVolumne", 1);
        }
        else
        {
            SFXSlider.value = PlayerPrefs.GetFloat("SFXVolumne");
        }
    }
    public void ChageVolume()
    {
        AudioListener.volume = musicSlider.value;
        PlayerPrefs.SetFloat("musicVolumne", musicSlider.value);
    }
    public void ChageSFXVolume()
    {
        PlayerPrefs.SetFloat("SFXVolumne", SFXSlider.value);
    }
    private void load()
    {
        musicSlider.value=PlayerPrefs.GetFloat("musicVolumne");
        Save();
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolumne", musicSlider.value);
    }
}
