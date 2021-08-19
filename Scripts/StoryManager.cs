using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    float finishtime = 0f;
    bool check = false;
    public GameObject FadeOut;
    public AudioSource TempAudio;

    void Start()
    {
        FadeOut.SetActive(false);
        GameObject.Find("SoundState").GetComponent<AudioSource>().Stop();
    }
    
    void Update()
    {
        finishtime = finishtime + Time.deltaTime;

        if ((finishtime >= 20.0f) && (check == false))
        {
            Click_SkipButton();
        }
        if((check == true) && (finishtime >= 1.0f))
        {
            Loading.loading("Menu");
        }
    }

    public void Click_SkipButton()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.Play();
        check = true;
        finishtime = 0.0f;
        FadeOut.SetActive(true);
    }
}
