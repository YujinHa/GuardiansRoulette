using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource audioSrc;
    private bool CheatCheck = false;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            if(!CheatCheck)
            {
                CheatCheck = true;
                audioSrc.Pause();
                Time.timeScale = 0f;
            }
            else
            {
                CheatCheck = false;
                audioSrc.UnPause();
                Time.timeScale = 1f;
            }
        }

        audioSrc.volume = SoundState.musicVolume;
    }
}
