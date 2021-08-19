using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HospitalManager : MonoBehaviour
{
    public GameObject m_BackButton;
    public GameObject NormalCanvas;
    public GameObject CoolTimeCanvas;
    public GameObject ItemQuestionCanvas;
    private PlayerState m_PlayerState;
    static public bool FinishedCoolTime = false;

    public GameObject FadeOut;
    float finishtime = 0f;
    bool check = false;
    private string NextScene;

    public GameObject GameExplainButton;
    public GameObject GameExplainCanvas;
   
    public AudioSource TempAudio;
    public AudioClip[] Clips;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject.Find("SoundState").GetComponent<AudioSource>().clip = GameObject.Find("SoundState").GetComponent<SoundState>().audioClip[1];
        GameObject.Find("SoundState").GetComponent<AudioSource>().Play();
        GameExplainCanvas.SetActive(false);
        FadeOut.SetActive(false);
        PlayerState.mapStates = 3;
        PlayerState.UseHospitalItem = false;
        m_PlayerState = GameObject.Find("PlayerState").GetComponent<PlayerState>();

        ItemQuestionCanvas.SetActive(false);

        if (PlayerState.coolTime_Hospital > 0)
        {
            CoolTimeCanvas.SetActive(true);
            NormalCanvas.SetActive(false);
        }
        else
        {
            CoolTimeCanvas.SetActive(false);
            NormalCanvas.SetActive(true);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    void Update()
    {
        if (check == true)
            finishtime = finishtime + Time.deltaTime;

        if (finishtime >= 1.0f)
            Loading.loading(NextScene);

        if ((PlayerState.coolTime_Hospital <= 0) && (FinishedCoolTime == false))
        {
            FinishCoolTime();
        }
    }

    private void FinishCoolTime()
    {
        FinishedCoolTime = true;
        CoolTimeCanvas.SetActive(false);
        NormalCanvas.SetActive(true);
    }

    public void EnterStore()
    {
        m_BackButton.SetActive(true);
    }

    public void BackClick_Hospital()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(Clips[1]);

        m_BackButton.SetActive(false);

        PlayerState.SetVillagePlayerPosition(new Vector3(10f, -0.2f, -8.3f), new Vector3 (0f, -90f, 0f));
        check = true;
        FadeOut.SetActive(true);
        NextScene = "Village";
    }

    public void OKClick_Hospital()
    {
        if (ItemState.ItemCount[6] > 0)
        {
            TempAudio.volume = SoundState.musicVolume;
            TempAudio.PlayOneShot(Clips[2]);
            NormalCanvas.SetActive(false);
            ItemQuestionCanvas.SetActive(true);
        }
        else
        {
            TempAudio.volume = SoundState.musicVolume;
            TempAudio.PlayOneShot(Clips[0]);
            LoadManiGame();
        }
    }

    public void UseItemClick_Hospital()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(Clips[0]);
        PlayerState.UseHospitalItem = true;
        LoadManiGame();
    }

    public void UnuseItemClick_Hospital()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(Clips[0]);
        PlayerState.UseHospitalItem = false;
        LoadManiGame();
    }

    private void LoadManiGame()
    {
        FinishedCoolTime = false;
        PlayerState.coolTime_Hospital = 600.0f;
        check = true;
        FadeOut.SetActive(true);
        NextScene = "fruits";
    }

    public void ClickGameExplainButton()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(Clips[2]);
        GameExplainCanvas.SetActive(true);
    }

    public void ClickGameExplainXButton()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(Clips[1]);
        GameExplainCanvas.SetActive(false);
    }
}
