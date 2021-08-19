using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public Text CurrentQuestText;
    public Text CoinText;
    public Text NameText;
    public Text LvText;
    public Image NameImage;
    public Sprite[] NameImages;

    public GameObject WhiteBackground;
    public GameObject BagCanvas;
    public GameObject SettingCanvas;
    public GameObject InformCanvas;

    public Text TutorialText1;
    public Text TutorialText2;
    public GameObject NextButton;
    public GameObject BeforeButton;
    public GameObject TutorialImage;

    public GameObject HPBar;
    public GameObject ExpBar;
    private Vector3 localScale_HP;
    private Vector3 localScale_Exp;

    public AudioSource TempAudio;
    public AudioClip[] clips;
    private bool SettingCheck = false;
    private bool BagCheck = false;

    void Start()
    {
        NameText.text = PlayerState.Name[PlayerState.goal];
        localScale_HP = HPBar.transform.localScale;
        localScale_Exp = ExpBar.transform.localScale;
        NameImage.sprite = NameImages[PlayerState.goal];
        WhiteBackground.SetActive(false);
        BagCanvas.SetActive(false);
        SettingCanvas.SetActive(false);
        InformCanvas.SetActive(false);
        LvText.text = (PlayerState.ExpLv + 1).ToString();
    }

    //필요할 때만 호출로 바꾸기
    void Update()
    {
        ChangeQuestUI();
        ChangeCoinUI();
        LvUIUpdate();

        localScale_HP.x = PlayerState.HP / (float)20.0;
        HPBar.transform.localScale = localScale_HP;

        localScale_Exp.x = PlayerState.Exp / PlayerState.MaxExp[PlayerState.ExpLv] * 5;
        ExpBar.transform.localScale = localScale_Exp;

        if (!GameObject.Find("FadeOut"))
        {
            if (Input.GetButtonDown("Fire2"))
            {
                if (!BagCheck)
                {
                    BagCheck = true;
                    Click_BagButton();
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Click_BagXButton();
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!SettingCheck)
                {
                    SettingCheck = true;
                    Click_SettingButton();
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Click_SettingXButton();
                }
            }
        }
    }

    public void LvUIUpdate()
    {
        LvText.text = "Lv." + (PlayerState.ExpLv + 1).ToString();
    }

    public void ChangeQuestUI()
    {
        CurrentQuestText.text = PlayerState.CurrentQuest.ToString() + " / " + PlayerState.TotalQuest[PlayerState.goal].ToString();
    }

    public void ChangeCoinUI()
    {
        CoinText.text = PlayerState.Coin.ToString();
    }

    public void Click_BagButton()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[0]);
        WhiteBackground.SetActive(true);
        PlayerState.ispaused = true;
        Time.timeScale = 0f;
        BagCanvas.SetActive(true);
        BagCanvas.GetComponent<BagManager>().IntoBag();
    }

    public void Click_BagXButton()
    {
        BagCheck = false;
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[1]);

        if (!SettingCheck)
        {
            WhiteBackground.SetActive(false);
            PlayerState.ispaused = false;
            Time.timeScale = 1f;
            if (PlayerState.mapStates != 3)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        BagCanvas.SetActive(false);
    }

    public void Click_SettingButton()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[0]);
        WhiteBackground.SetActive(true);
        PlayerState.ispaused = true;
        Time.timeScale = 0f;
        SettingCanvas.SetActive(true);
    }

    public void Click_SettingXButton()
    {
        SettingCheck = false;
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[1]);
        if (!BagCheck)
        {
            if (PlayerState.mapStates != 3)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            WhiteBackground.SetActive(false);
            PlayerState.ispaused = false;
            Time.timeScale = 1f;
        }
        InformCanvas.SetActive(false);
        SettingCanvas.SetActive(false);
    }

    //셋팅 창의 게임 방법 버튼 눌렀을 때
    public void Click_InformButton_Setting()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[0]);
        InformCanvas.SetActive(true);
        NextButton.SetActive(true);
        BeforeButton.SetActive(false);
        TutorialText1.enabled = true;
        TutorialText2.enabled = false;
        TutorialImage.SetActive(false);
    }

    public void Click_InformXButton_Setting()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[1]);
        InformCanvas.SetActive(false);
    }

    public void Click_InformNextButton_Setting()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[1]);
        TutorialText1.enabled = false;
        TutorialText2.enabled = true;
        TutorialImage.SetActive(true);
        NextButton.SetActive(false);
        BeforeButton.SetActive(true);
    }

    public void Click_InformBeforeButton_Setting()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[1]);
        TutorialText1.enabled = true;
        TutorialText2.enabled = false;
        TutorialImage.SetActive(false);
        NextButton.SetActive(true);
        BeforeButton.SetActive(false);
    }

    //셋팅 창의 종료 버튼
    public void Click_QuitButton_Setting()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[1]);
        Application.Quit();
    }
    
}
