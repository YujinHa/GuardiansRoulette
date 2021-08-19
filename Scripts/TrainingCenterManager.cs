using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrainingCenterManager : MonoBehaviour
{
    public GameObject m_BackButton;
    public GameObject OKButton;
    public GameObject GameExplainButton;
    public GameObject GameExplainCanvas;
    public Text ExplinText;
    private PlayerState m_PlayerState;
    public GameObject NormalCanvas;
    public GameObject ItemQuestionCanvas;
    public Text ItemInformText;
    public Text SkillPriceText;
    public Image SkillPrice;
    public Image[] SkillImages;
    private int CurrentSkillNum;
    private int[] SkillPrices = { 5, 10, 15, 5, 10, 15, 5, 10, 15 };

    public GameObject FadeOut;
    float finishtime = 0f;
    bool check = false;
    private string NextScene;

    public AudioSource TempAudio;
    public AudioClip[] clips;

    private string[] SkillInform =
    {
        "불스킬 1단계\n\n플레이어의 공격 범위가 일반 공격보다 넓어집니다.",
        "불스킬 2단계\n\n대전의 하늘에서 불구덩이가 마구 떨어져 적을 공격합니다.",
        "불스킬 3단계\n\n대전에 있는 모든 적의 HP를 15씩 줄입니다.",
        "얼음스킬 1단계\n\n플레이어의 공격 범위가 일반 공격보다 넓어집니다.",
        "얼음스킬 2단계\n\n대전의 하늘에서 얼음조각이 마구 떨어져 적을 공격합니다.",
        "얼음스킬 3단계\n\n대전에 있는 모든 적의 HP를 15씩 줄입니다.",
        "정령스킬 1단계\n\n플레이어는 5초간 공격을 받아도 HP가 닳지 않습니다.",
        "정령스킬 2단계\n\n플레이어를 8초간 투명으로 만들어줍니다.",
        "정령스킬 3단계\n\n플레이어가 10초동안 받는 공격만큼\n플레이어의 HP가 회복되고 적의 HP는 줄어듭니다.",
    };

    private string[] GameExplain =
    {
        "Fly 게임\n\n불 스킬을 연마하러 오셨군요.\n\n호박폭탄들이 길을 막고 있습니다.\n\n하늘을 날면서 Fire총을 이용하여 폭탄들을 제거하세요!\n\n" +
            "좌우 키보드를 이용하여 피하셔도 괜찮습니다.\n\n폭탄을 피하거나 제거하지 못하고 부딪히면\n\n" +
            "즉시 게임이 종료되고 스킬 획득에 실패합니다.\n\n건투를 빕니다!",

        "Chess 게임\n\n얼음 스킬을 연마하러 오셨군요.\n\n체스적들이 랜덤하게 생깁니다.\n\nIce총을 이용하여 적들을 제거하세요!\n\n" +
            "우측 상단에 제거해야되는 목표가 적혀있습니다.\n\n제한시간 내에 목표값에 도달하지 못 할 경우, 스킬\n\n" +
            "획득에 실패합니다.\n\n제한시간 내에 체스 적들을 제거해주세요.\n\n건투를 빕니다!",

        "Cannon 게임\n\n정령 스킬을 연마하러 오셨군요.\n\n앞에 있는 대포에서 총알들이 날라옵니다.\n\n상하좌우 키보드를 이용하여 총알들을 피해주세요.\n\n" +
            "타이머가 종료되기 전에 총알에 맞게되면\n\n즉시 게임이 종료되고 스킬 획득에 실패합니다.\n\n건투를 빕니다!"
    };

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject.Find("SoundState").GetComponent<AudioSource>().clip = GameObject.Find("SoundState").GetComponent<SoundState>().audioClip[5];
        GameObject.Find("SoundState").GetComponent<AudioSource>().Play();
        FadeOut.SetActive(false);
        PlayerState.mapStates = 3;
        PlayerState.UseHospitalItem = false;
        m_PlayerState = GameObject.Find("PlayerState").GetComponent<PlayerState>();
        NormalCanvas.SetActive(true);
        ItemQuestionCanvas.SetActive(false);
        SkillPrice.enabled = false;
        ItemInformText.text = "";
        SkillPriceText.text = "";
        OKButton.SetActive(false);
        GameExplainButton.SetActive(false);
        GameExplainCanvas.SetActive(false);
        ExplinText.text = "";

        if (PlayerState.ExpLv == 0)
        {
            for (int i = 0; i < 9; i++)
            {
                if((i % 3 == 1) || (i % 3 == 2) || PlayerState.GotSkill[i])
                    SkillImages[i].color = Color.gray;
                else
                    SkillImages[i].color = Color.white;
            }
        }
        else if (PlayerState.ExpLv == 1)
        {
            for (int i = 0; i < 9; i++)
            {
                if ((i % 3 == 2) || (PlayerState.GotSkill[i]))
                    SkillImages[i].color = Color.gray;
                else
                    SkillImages[i].color = Color.white;
            }
        }
        else
        {
            for (int i = 0; i < 9; i++)
            {
                if(PlayerState.GotSkill[i])
                    SkillImages[i].color = Color.gray;
                else
                    SkillImages[i].color = Color.white;
            }
        }
    }

    void Update()
    {
        if (check == true)
            finishtime = finishtime + Time.deltaTime;

        if (finishtime >= 1.0f)
            Loading.loading(NextScene);
    }
    
    public void BackClick_TrainingCenter()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[1]);

        PlayerState.SetVillagePlayerPosition(new Vector3(-1.09f, 0f, 16.1f), new Vector3(0f, 90f, 0f));

        check = true;
        FadeOut.SetActive(true);
        NextScene = "Village";
    }

    public void SkillClick(int num)
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[0]);
        CurrentSkillNum = num;
        ItemInformText.text = SkillInform[num];
        SkillPrice.enabled = true;
        SkillPriceText.text = SkillPrices[CurrentSkillNum].ToString();
        
        if ((PlayerState.Coin - SkillPrices[CurrentSkillNum] >= 0) && ((num % 3) <= PlayerState.ExpLv) && (!PlayerState.GotSkill[num]))
        {
            OKButton.SetActive(true);
            GameExplainButton.SetActive(true);
        }
        else
        {
            OKButton.SetActive(false);
            GameExplainButton.SetActive(false);
        }
    }

    public void OKClick_Hospital()
    {
        PlayerState.Coin -= SkillPrices[CurrentSkillNum];

        if (ItemState.ItemCount[7] > 0)
        {
            TempAudio.volume = SoundState.musicVolume;
            TempAudio.PlayOneShot(clips[0]);
            NormalCanvas.SetActive(false);
            ItemQuestionCanvas.SetActive(true);
        }
        else
        {
            TempAudio.volume = SoundState.musicVolume;
            TempAudio.PlayOneShot(clips[2]);
            LoadManiGame();
        }
    }

    public void UseItemClick_Hospital()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[2]);
        PlayerState.UseTrainingCenterItem = true;
        LoadManiGame();
    }

    public void UnuseItemClick_Hospital()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[2]);
        PlayerState.UseTrainingCenterItem = false;
        LoadManiGame();
    }

    //스킬 얻었을 때 GotSkill값 갱신해주기
    private void LoadManiGame()
    {
        check = true;
        FadeOut.SetActive(true);

        PlayerState.MiniGameChoice = CurrentSkillNum % 3;
        if ((CurrentSkillNum == 0) || (CurrentSkillNum == 1) || (CurrentSkillNum == 2))
        {
            NextScene = "Fly";
        }
        else if ((CurrentSkillNum == 3) || (CurrentSkillNum == 4) || (CurrentSkillNum == 5))
        {
            NextScene = "Chess";
        }
        else if ((CurrentSkillNum == 6) || (CurrentSkillNum == 7) || (CurrentSkillNum == 8))
        {
            NextScene = "Cannons";
        }
    }

    public void ClickGameExplainButton()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[0]);
        GameExplainCanvas.SetActive(true);

        if ((CurrentSkillNum == 0) || (CurrentSkillNum == 1) || (CurrentSkillNum == 2))
            ExplinText.text = GameExplain[0];
        else if ((CurrentSkillNum == 3) || (CurrentSkillNum == 4) || (CurrentSkillNum == 5))
            ExplinText.text = GameExplain[1];
        else if ((CurrentSkillNum == 6) || (CurrentSkillNum == 7) || (CurrentSkillNum == 8))
            ExplinText.text = GameExplain[2];
    }

    public void ClickGameExplainXButton()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[1]);
        GameExplainCanvas.SetActive(false);
    }
}
