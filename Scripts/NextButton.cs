using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    public GameObject FadeOut;
    float finishtime = 0f;
    bool check = false;
    private string NextScene;

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
            Time.timeScale = 1f;
            Loading.loading(NextScene);
        }

    }
    
    public void NextClick_Hospital()
    {
        GameObject.Find("Player").GetComponent<AudioSource>().volume = SoundState.musicVolume;
        GameObject.Find("Player").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("Player").GetComponent<Timer>().Clips[1]);
        finishtime = 0f;
        check = true;
        FadeOut.SetActive(true);
        NextScene = "Hospital";
    }
}
