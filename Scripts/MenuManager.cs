using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject SelectGoalName;
    public GameObject TutorialCanvas;
    public GameObject BlackBackground;
    public Text TutorialText1;
    public Text TutorialText2;
    public GameObject TutorialImage;
    public GameObject NextButton;
    public GameObject BeforeButton;
    public AudioSource TempAudio;
    public AudioClip[] clips;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SelectGoalName.SetActive(false);
        TutorialCanvas.SetActive(false);
        BlackBackground.SetActive(false);
        GameObject.Find("SoundState").GetComponent<AudioSource>().clip = GameObject.Find("SoundState").GetComponent<SoundState>().audioClip[11];
        GameObject.Find("SoundState").GetComponent<AudioSource>().Play();
    }
    
    void Update()
    {
    }

    public void StartButton()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[1]);
        SelectGoalName.SetActive(true);
        BlackBackground.SetActive(true);
    }

    public void TutorialButton()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[1]);
        TutorialCanvas.SetActive(true);
        BlackBackground.SetActive(true);
        TutorialText1.enabled = true;
        TutorialText2.enabled = false;
        TutorialImage.SetActive(false);
        NextButton.SetActive(true);
        BeforeButton.SetActive(false);
    }

    public void QuitButton()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[0]);
        Application.Quit();
    }

    public void XButton_SelectName()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[0]);
        SelectGoalName.SetActive(false);
        BlackBackground.SetActive(false);
    }

    public void XButton_Tutorial()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[0]);
        TutorialCanvas.SetActive(false);
        BlackBackground.SetActive(false);
    }

    public void NextButton_Tutorial()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[0]);
        TutorialText1.enabled = false;
        TutorialText2.enabled = true;
        TutorialImage.SetActive(true);
        NextButton.SetActive(false);
        BeforeButton.SetActive(true);
    }

    public void BeforeButton_Tutorial()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[0]);
        TutorialText1.enabled = true;
        TutorialText2.enabled = false;
        TutorialImage.SetActive(false);
        NextButton.SetActive(true);
        BeforeButton.SetActive(false);
    }
}
