using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalSelect : MonoBehaviour
{
    public GameObject FadeOut;
    float finishtime = 0f;
    bool check = false;
    public AudioSource TempAudio;
    public AudioClip selectClip;
    
    void Start()
    {
        FadeOut.SetActive(false);
    }
    
    void Update()
    {
        if (check == true)
            finishtime = finishtime + Time.deltaTime;

        if (finishtime >= 1.0f)
        {
            Loading.loading("Village");
        }
    }

    public void GoalNameButton(int GoalName)
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(selectClip);
        PlayerState.goal = GoalName;
        PlayerState.SetStart();
        ItemState.SetStart();
        PetState.SetStart();
        check = true;
        FadeOut.SetActive(true);
    }
}
