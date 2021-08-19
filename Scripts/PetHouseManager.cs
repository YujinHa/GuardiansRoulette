using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PetHouseManager : MonoBehaviour
{
    public GameObject m_BackButton;
    public GameObject m_AccompanyButton;
    public GameObject m_UseItemButton;
    public GameObject m_ExpBarSet;
    public GameObject m_ExpBar;
    public GameObject m_HealthBarSet;
    public GameObject m_HealthBar;
    public GameObject m_PetList;
    public GameObject m_ItemPanel;

    private PlayerState m_PlayerState;
    private int currentSelectPet = 6;//현재 펫 선택값(초기값:6(디폴트값))
    private int tempcurrentSelectPet = 0;
    public GameObject[] Pets;
    public Text[] AccompanyInform;//펫리스트에 동행중인지 아닌지 보여주는 UI
    public Text PetInformText;
    public Text PetItemInformText;
    public Text CurrentAccompanyText;
    public Text HealthBarText;
    public Text[] ItemCountText;
    public Text PetLvText;
    public int CurrentItemNum;
    private Vector3 localScale_Health;
    private Vector3 localScale_Exp;
    public Image[] PetEnableImages;//7번 아이템은 던전에서만 사용 가능
    public Image[] PetDisableImages;//7번 아이템은 던전에서만 사용 가능 / 펫하우스에서는 항상 회색조

    public GameObject FadeOut;
    float finishtime = 0f;
    bool check = false;
    private string NextScene;
    public AudioSource TempAudio;
    public AudioClip[] Clips;
    private bool TempCheck = false;

    private string[] PetInformTexts = { "알\n\n아이템을 사용해서 알을 돌보면\n펫으로 부화할 수 있어요\n\n부화할 펫의 종류는 랜덤입니다",
        "스파이크\n\n능력\n플레이어의 공격력 증가\n\nLv1.\n플레이어의 공격력 +1\n\nLv2.\n플레이어의 공격력 +1.5\n\nLv3.\n플레이어의 공격력 +2",
        "데빌\n\n능력\n스킬 쿨타임 감소\n\nLv1.\n쿨타임 2초 감소\n\nLv2.\n쿨타임 4초 감소\n\nLv3.\n쿨타임 6초 감소",
        "나뭇잎 요정\n\n능력\n보상 골드 증가\n\nLv1.\n보상 골드 +1\n\nLv2.\n보상 골드 +2\n\nLv3.\n보상 골드 +3",
        "폭스\n\n능력\n플레이어의 방어력 증가\n\nLv1.\n받는 데미지 10% 감소\n\nLv2.\n받는 데미지 20% 감소\n\nLv3.\n받는 데미지 30% 감소",
        "펌킨\n\n능력\n적 공격 빈도 감소\n\nLv1.\n적 공격 빈도: 1.5초에 1번\n\nLv2.\n적 공격 빈도: 2초에 1번\n\nLv3.\n적 공격 빈도: 3초에 1번" };

    private string[] PetItemTexts = {"곰젤리\n\n효과\n펫 체력 +1\n\n펫에게만 사용 가능",
    "고양이쿠키\n\n효과\n펫 체력 +3\n\n펫에게만 사용 가능",
    "아이스크림\n\n효과\n펫 체력 최대치로 회복\n\n펫에게만 사용 가능",
    "마포\n\n효과\n알 성장 +1\n\n알에만 사용 가능",
    "실\n\n효과\n알 성장 +5\n\n알에만 사용 가능",
    "담요\n\n효과\n알 성장 최대치 증가\n사용 즉시 펫으로 부화 가능\n\n알에만 사용 가능",
    "펫 경험치 증가 아이템\n\n효과\n대전에 동행하는 펫이 획득하는\n경험치 1.5배 증가\n\n이 아이템을 사용하는\n퀘스트 동안의 보상에만 효력 작용\n중복 적용 불가",
    "전체 동행 펫 수 증가 아이템\n\n효과\n플레이어와 함께 던전에\n동행할 수 있는 펫 최대수 증가\n\n누적 사용 가능"};

    void Start()
    {
        GameObject.Find("SoundState").GetComponent<AudioSource>().clip = GameObject.Find("SoundState").GetComponent<SoundState>().audioClip[6];
        GameObject.Find("SoundState").GetComponent<AudioSource>().Play();
        FadeOut.SetActive(false);
        PlayerState.mapStates = 3;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        m_PlayerState = GameObject.Find("PlayerState").GetComponent<PlayerState>();
        m_AccompanyButton.SetActive(false);
        m_UseItemButton.SetActive(false);
        m_HealthBarSet.SetActive(false);
        m_ExpBarSet.SetActive(false);
        
        CurrentAccompanyText.text = PetState.PetNum.ToString() + " / " + PetState.AbleTotalPetNum.ToString();
        PetItemInformText.text = "";
        localScale_Health = m_HealthBar.transform.localScale;
        localScale_Exp = m_ExpBar.transform.localScale;

        for (int i = 0; i < 8; i++)
        {
            ItemCountText[i].text = ItemState.ItemCount[i + 8].ToString();
        }

        if (PetState.IsFullPet)
        {
            PetEnableImages[0].enabled = false;
            PetDisableImages[0].enabled = true;
        }
        else
        {
            PetEnableImages[0].enabled = true;
            PetDisableImages[0].enabled = false;
        }
        
        for (int i = 1; i < 6; i++)
        {
            if (PetState.PetStates[i - 1] == 0)
            {
                PetEnableImages[i].enabled = false;
                PetDisableImages[i].enabled = true;
            }
            else
            {
                PetEnableImages[i].enabled = true;
                PetDisableImages[i].enabled = false;
            }
            if(PetState.PetStates[i - 1] != 2)
                AccompanyInform[i - 1].text = "";
            else
                AccompanyInform[i - 1].text = "동행";
        }
    }

    void Update()
    {
        if (check == true)
            finishtime = finishtime + Time.deltaTime;
        
        if (finishtime >= 1.0f)
            Loading.loading(NextScene);
    }

    public void EnterPetHouse()
    {
        m_BackButton.SetActive(true);
        m_AccompanyButton.SetActive(false);
    }

    public void BackClick_PetHouse()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(Clips[0]);
        m_BackButton.SetActive(false);

        PlayerState.SetVillagePlayerPosition(new Vector3(7.8f, -0.2f, -16.3f), new Vector3(0f, 0f, 0f));
        check = true;
        FadeOut.SetActive(true);
        NextScene = "Village";
    }

    public void PetClick(int num)
    {
        tempcurrentSelectPet = num;

        if (num == 0)
        {
            if (PetEnableImages[0].enabled == true)
            {
                TempAudio.volume = SoundState.musicVolume;
                TempAudio.PlayOneShot(Clips[1]);
                currentSelectPet = tempcurrentSelectPet;
                PetInformText.text = PetInformTexts[num];

                m_AccompanyButton.SetActive(false);
                m_ExpBarSet.SetActive(false);
                m_HealthBarSet.SetActive(true);

                Destroy(GameObject.Find("CurrentPet"));
                GameObject CurrentPet = Instantiate(Pets[num]) as GameObject;
                CurrentPet.name = "CurrentPet";
                HealthBarText.text = "알 성장률";

                localScale_Health.x = PetState.HatchValue / (float)4.0;
                m_HealthBar.transform.localScale = localScale_Health;

                ItemClick(CurrentItemNum);
            }
        }
        else if(num == 6)
        {
            m_HealthBarSet.SetActive(false);
            PetInformText.text = "";
            PetLvText.text = "";
            Destroy(GameObject.Find("CurrentPet"));
            m_UseItemButton.SetActive(false);
        }
        else
        {
            if (PetState.PetStates[tempcurrentSelectPet - 1] != 0)
            {
                TempAudio.volume = SoundState.musicVolume;
                TempAudio.PlayOneShot(Clips[1]);
                currentSelectPet = tempcurrentSelectPet;
                m_AccompanyButton.SetActive(true);
                m_ExpBarSet.SetActive(true);
                m_HealthBarSet.SetActive(true);

                PetInformText.text = PetInformTexts[num];
                PetLvText.text = "Lv." + (PetState.Lv[num - 1] + 1).ToString();

                //띄워진 오브젝트 지우고 펫1 오브젝트 띄우기
                Destroy(GameObject.Find("CurrentPet"));
                GameObject CurrentPet = Instantiate(Pets[num]) as GameObject;
                CurrentPet.name = "CurrentPet";
                HealthBarText.text = "펫 체력";

                localScale_Health.x = PetState.Health[currentSelectPet - 1] / (float)4.0;
                m_HealthBar.transform.localScale = localScale_Health;

                localScale_Exp.x = PetState.Exp[currentSelectPet - 1] / (float)4.0;
                m_ExpBar.transform.localScale = localScale_Exp;
                ItemClick(CurrentItemNum);
            }
        }
    }

    public void AccompanyClick()
    {
        TempAudio.volume = SoundState.musicVolume;

        if ((PetState.PetStates[currentSelectPet - 1] == 1) && (PetState.PetNum < PetState.AbleTotalPetNum))//동행하지 않는 상태면 동행
        {
            TempAudio.PlayOneShot(Clips[2]);
            PetState.PetNum++;
            CurrentAccompanyText.text = PetState.PetNum.ToString() + " / " + PetState.AbleTotalPetNum.ToString();

            PetState.PetStates[currentSelectPet - 1] = 2;
            AccompanyInform[currentSelectPet - 1].text = "동행";
        }
        else if (PetState.PetStates[currentSelectPet - 1] == 2)//동행하고 있는 상태면 동행에서 빼기
        {
            TempAudio.PlayOneShot(Clips[0]);
            PetState.PetNum--;
            CurrentAccompanyText.text = PetState.PetNum.ToString() + " / " + PetState.AbleTotalPetNum.ToString();

            PetState.PetStates[currentSelectPet - 1] = 1;
            AccompanyInform[currentSelectPet - 1].text = "";
        }
    }

    public void ItemClick(int num)
    {
        if (!TempCheck)
        {
            TempAudio.volume = SoundState.musicVolume;
            TempAudio.PlayOneShot(Clips[1]);
        }
        else
            TempCheck = false;
        CurrentItemNum = num;
        PetItemInformText.text = PetItemTexts[num];

        if ((num == 0) || (num == 1) || (num == 2))
        {
            if ((currentSelectPet == 0) || (currentSelectPet == 6))
                m_UseItemButton.SetActive(false);
            else
                m_UseItemButton.SetActive(true);
        }
        else if ((num == 3) || (num == 4) || (num == 5))
        {
            if ((currentSelectPet == 0) && (PetDisableImages[0].enabled == false))
                m_UseItemButton.SetActive(true);
            else
                m_UseItemButton.SetActive(false);
        }
        else if (num == 6)
            m_UseItemButton.SetActive(false);
        else if (num == 7)
        {
            if (PetState.AbleTotalPetNum < 5)
                m_UseItemButton.SetActive(true);
            else
                m_UseItemButton.SetActive(false);
        }
    }

    public void UseItemClick()
    {
        if (ItemState.ItemCount[CurrentItemNum + 8] > 0)
        {
            TempAudio.volume = SoundState.musicVolume;
            TempAudio.PlayOneShot(Clips[2]);
            if (CurrentItemNum == 0)
            {
                if (PetState.Health[currentSelectPet - 1] < 10)
                {
                    PetState.Health[currentSelectPet - 1] = ((PetState.Health[currentSelectPet - 1]++ <= 10) ? PetState.Health[currentSelectPet - 1]++ : 10);
                    ItemState.ItemCount[CurrentItemNum + 8]--;
                    ShowHealthBar();
                }
            }
            else if (CurrentItemNum == 1)
            {
                if (PetState.Health[currentSelectPet - 1] < 10)
                {
                    PetState.Health[currentSelectPet - 1] = (((PetState.Health[currentSelectPet - 1] + 3) <= 10) ? (PetState.Health[currentSelectPet - 1] + 3) : 10);
                    ItemState.ItemCount[CurrentItemNum + 8]--;
                    ShowHealthBar();
                }
            }
            else if (CurrentItemNum == 2)
            {
                if (PetState.Health[currentSelectPet - 1] < 10)
                {
                    PetState.Health[currentSelectPet - 1] = 10;
                    ItemState.ItemCount[CurrentItemNum + 8]--;
                    ShowHealthBar();
                }
            }
            else if (CurrentItemNum == 3)
            {
                if (PetEnableImages[0].IsActive())//알 회색조 아니면
                {
                    PetState.HatchValue++;
                    CheckHatch();
                    ItemState.ItemCount[CurrentItemNum + 8]--;
                }
            }
            else if (CurrentItemNum == 4)
            {
                if (PetEnableImages[0].IsActive())//알 회색조 아니면
                {
                    PetState.HatchValue += 5;
                    CheckHatch();
                    ItemState.ItemCount[CurrentItemNum + 8]--;
                }
            }
            else if (CurrentItemNum == 5)
            {
                if (PetEnableImages[0].IsActive())//알 회색조 아니면
                {
                    PetState.HatchValue = 10;
                    CheckHatch();
                    ItemState.ItemCount[CurrentItemNum + 8]--;
                }
            }
            else if (CurrentItemNum == 7)
            {
                if (PetState.AbleTotalPetNum < 5)
                {
                    PetState.AbleTotalPetNum++;
                    ItemState.ItemCount[CurrentItemNum + 8]--;
                    CurrentAccompanyText.text = PetState.PetNum.ToString() + " / " + PetState.AbleTotalPetNum.ToString();
                }
            }
            ItemCountText[CurrentItemNum].text = ItemState.ItemCount[CurrentItemNum + 8].ToString();
        }
        TempCheck = true;
        ItemClick(CurrentItemNum);
    }
    
    //HatchValue가 MaxHatchValue보다 커지면 펫을 랜덤으로 선택해서 부화시킴
    public void CheckHatch()
    {
        if (PetState.HatchValue >= PetState.MaxHatchValue)
        {
            while (true)
            {
                int rand = Random.Range(0, 5);

                if (PetState.PetStates[rand] == 0)
                {
                    PetState.PetStates[rand] = 1;
                    PetState.GotPetNum++;

                    //펫리스트 컬러Image로
                    PetDisableImages[rand + 1].enabled = false;
                    PetEnableImages[rand + 1].enabled = true;

                    PetClick(6);

                    //Get 애니메이션
                    GameObject CurrentPet = Instantiate(Pets[rand + 1]) as GameObject;
                    CurrentPet.name = "CurrentPet";

                    Animator Anim = CurrentPet.GetComponent<Animator>();
                    Anim.SetTrigger("get");

                    TempAudio.volume = SoundState.musicVolume;
                    TempAudio.PlayOneShot(Clips[3]);

                    m_ItemPanel.SetActive(false);
                    m_PetList.SetActive(false);
                    currentSelectPet = rand + 1;
                    Invoke("ResetAfterGet", 3f);

                    break;
                }
            }
            PetState.HatchValue = 0;
        }

        ShowGrowBar();

        //모든 펫이 다 있을 경우 return
        for (int i = 0; i < PetState.TotalPetNum; i++)
        {
            if (PetState.PetStates[i] == 0)
                break;
            else if (i == PetState.TotalPetNum - 1)
            {
                //펫리스트 알 회색조로
                PetEnableImages[0].enabled = false;
                PetDisableImages[0].enabled = true;
                return;
            }
        }
    }

    private void ResetAfterGet()
    {
        m_ItemPanel.SetActive(true);
        m_PetList.SetActive(true);
        PetClick(currentSelectPet);
    }

    private void ShowGrowBar()
    {
        localScale_Health.x = PetState.HatchValue / (float)4.0;
        m_HealthBar.transform.localScale = localScale_Health;
    }

    private void ShowHealthBar()
    {
        localScale_Health.x = PetState.Health[currentSelectPet - 1] / (float)4.0;
        m_HealthBar.transform.localScale = localScale_Health;
    }
}
