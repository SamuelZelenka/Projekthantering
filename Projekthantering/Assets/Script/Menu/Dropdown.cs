
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class Dropdown : MonoBehaviour
{
    Resolution[] resolutions;

    public Dropdown dropDownMenu;

    public AudioMixer mixer;

    void Start()
    {
        resolutions = Screen.resolutions;
        

        for (int i = 0; i < resolutions.Length; i++)
        {
            
        }
    }

    public void SetVolume (float volume)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }
}
