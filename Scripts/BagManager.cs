using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BagManager : MonoBehaviour
{
    public GameObject BagCanvas;
    public GameObject m_ItemSet;
    public GameObject m_PetItemSet;
    public GameObject UseButton;

    public Text ItemSelectButtonText;
    public Text PetItemSelectButtonText;
    public Text ItemInformtText;

    public int currentSelectItem;

    public GameObject pet_HealthBarSet;
    public GameObject pet_HealthBar;
    public GameObject PetSelected;
    private int currentSelectPet = 5;//초기값
    public Text[] AccompanyInform;//펫리스트에 동행중인지 아닌지 보여주는 UI
    public Text[] PetLevelTexts;
    public Text CurrentAccompanyText;
    public Text NoticeText;
    public Text[] ItemCountText;
    private Vector3 localScale_PetHealth;
    private int tempcurrentSelectPet = 0;
    public Image[] PetEnableImages;//7번 아이템은 던전에서만 사용 가능
    public Image[] PetDisableImages;//7번 아이템은 던전에서만 사용 가능 / 펫하우스에서는 항상 회색조
    
    public AudioSource TempAudio;
    public AudioClip[] clips;
    
    float finishtime = 0f;
    bool check = false;
    private string NextScene;

    void Start()
    {
        for (int i = 0; i < 16; i++)
        {
            ItemCountText[i].text = ItemState.ItemCount[i].ToString();
        }
        m_PetItemSet.SetActive(false);
        m_ItemSet.SetActive(true);
        ItemSelectButtonText.color = Color.yellow;
        PetItemSelectButtonText.color = Color.white;
        UseButton.SetActive(false);

        pet_HealthBarSet.SetActive(false);
        CurrentAccompanyText.text = PetState.PetNum.ToString() + " / " + PetState.AbleTotalPetNum.ToString();
        ItemInformtText.text = "";
        localScale_PetHealth = pet_HealthBar.transform.localScale;

        PetSelected.SetActive(false);

        for (int i = 0; i < 5; i++)
        {
            if (PetState.PetStates[i] == 0)
            {
                PetEnableImages[i].enabled = false;
                PetDisableImages[i].enabled = true;
            }
            else
            {
                PetEnableImages[i].enabled = true;
                PetDisableImages[i].enabled = false;

                if(PetState.PetStates[i] == 2)
                    AccompanyInform[i].text = "동행";
                else
                    AccompanyInform[i].text = "";
            }
        }
    }

    void Update()
    {
        //if (check == true)
        //    finishtime = finishtime + Time.deltaTime;

        //if (finishtime >= 1.0f)
        //    Loading.loading(NextScene);
    }

    public void IntoBag()
    {
        for (int i = 0; i < 16; i++)
        {
            ItemCountText[i].text = ItemState.ItemCount[i].ToString();
        }
        m_PetItemSet.SetActive(false);
        m_ItemSet.SetActive(true);
        ItemSelectButtonText.color = Color.yellow;
        PetItemSelectButtonText.color = Color.white;
        UseButton.SetActive(false);

        pet_HealthBarSet.SetActive(false);
        CurrentAccompanyText.text = PetState.PetNum.ToString() + " / " + PetState.AbleTotalPetNum.ToString();
        ItemInformtText.text = "";
        localScale_PetHealth = pet_HealthBar.transform.localScale;

        PetSelected.SetActive(false);

        for (int i = 0; i < 5; i++)
        {
            if (PetState.PetStates[i] == 0)
            {
                PetEnableImages[i].enabled = false;
                PetDisableImages[i].enabled = true;
            }
            else
            {
                PetEnableImages[i].enabled = true;
                PetDisableImages[i].enabled = false;

                if (PetState.PetStates[i] == 2)
                    AccompanyInform[i].text = "동행";
                else
                    AccompanyInform[i].text = "";

                PetLevelTexts[i].text = "Lv." + (PetState.Lv[i] + 1).ToString();
            }
        }
    }

    public void PetClick(int num)
    {
        if (PetItemSelectButtonText.color == Color.yellow)
        {
            tempcurrentSelectPet = num;

            if (PetState.PetStates[tempcurrentSelectPet] != 0)
            {
                TempAudio.volume = SoundState.musicVolume;
                TempAudio.PlayOneShot(clips[1]);
                currentSelectPet = tempcurrentSelectPet;
                pet_HealthBarSet.SetActive(true);

                localScale_PetHealth.x = PetState.Health[currentSelectPet] / (float)4.0;
                pet_HealthBar.transform.localScale = localScale_PetHealth;

                if ((currentSelectItem == 8) || (currentSelectItem == 9) || (currentSelectItem == 10))
                    ItemClick(currentSelectItem);

                if (currentSelectPet != 5)
                {
                    PetSelected.SetActive(true);

                    if (currentSelectPet == 0)
                        PetSelected.transform.localPosition = new Vector3(-362, -353, 0);
                    else if (currentSelectPet == 1)
                        PetSelected.transform.localPosition = new Vector3(-182, -353, 0);
                    else if (currentSelectPet == 2)
                        PetSelected.transform.localPosition = new Vector3(-10, -353, 0);
                    else if (currentSelectPet == 3)
                        PetSelected.transform.localPosition = new Vector3(181, -353, 0);
                    else if (currentSelectPet == 4)
                        PetSelected.transform.localPosition = new Vector3(358, -353, 0);
                }
                else
                {
                    PetSelected.SetActive(false);
                }
            }
        }
    }

    public void ItemClick(int num)
    {
        if ((PlayerState.mapStates == 0) || (PlayerState.mapStates == 3))
            ItemClick_VillageOrStore(num);
        else if (PlayerState.mapStates == 1)
            ItemClick_Dongeon(num);
        else
            ItemClick_Battle(num);
    }

    public void ItemClick_VillageOrStore(int num)
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[1]);
        currentSelectItem = num;
        ItemInformtText.text = ItemState.ItemInforms[currentSelectItem].ToString();
        NoticeText.enabled = true;

        if ((num == 0) || (num == 1) || (num == 3))
        {
            NoticeText.text = "";
            if (ItemState.ItemCount[currentSelectItem] > 0)
                UseButton.SetActive(true);
            else
                UseButton.SetActive(false);
        }
        else if ((num == 8) || (num == 9) || (num == 10))
        {
            if ((currentSelectPet == 5) || (ItemState.ItemCount[currentSelectItem] == 0))
            {
                UseButton.SetActive(false);
                NoticeText.text = "";
                if (currentSelectPet == 5)
                    NoticeText.text = "아이템을 사용할 펫을 선택해주세요\n동행한 펫에게만 사용 가능";
            }
            else if (PetState.PetStates[currentSelectPet] != 2)
            {
                UseButton.SetActive(false);
                NoticeText.text = "아이템을 사용할 펫을 선택해주세요\n동행한 펫에게만 사용 가능";
            }
            else
            {
                UseButton.SetActive(true);
                NoticeText.text = "";
            }
        }
        else
        {
            UseButton.SetActive(false);

            if (num == 2)
                NoticeText.text = "대전에서만 사용 가능";
            else if ((num == 4) || (num == 5) || (num == 14))
                NoticeText.text = "던전에서만 사용 가능";
            else if (num == 6)
                NoticeText.text = "병원에서만 사용 가능";
            else if (num == 7)
                NoticeText.text = "수련장에서만 사용 가능";
            else
                NoticeText.text = "펫 하우스에서만 사용 가능";
        }
    }

    public void ItemClick_Dongeon(int num)
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[1]);
        currentSelectItem = num;
        ItemInformtText.text = ItemState.ItemInforms[currentSelectItem].ToString();
        NoticeText.enabled = true;

        if ((num == 0) || (num == 1) || (num == 4) || (num == 5))
        {
            NoticeText.text = "";
            if (ItemState.ItemCount[currentSelectItem] > 0)
                UseButton.SetActive(true);
            else
                UseButton.SetActive(false);
        }
        else if ((num == 8) || (num == 9) || (num == 10))
        {
            if ((currentSelectPet == 5) || (ItemState.ItemCount[currentSelectItem] == 0))
            {
                UseButton.SetActive(false);
                NoticeText.text = "";
                if (currentSelectPet == 5)
                    NoticeText.text = "아이템을 사용할 펫을 선택해주세요\n동행한 펫에게만 사용 가능";
            }
            else if (PetState.PetStates[currentSelectPet] != 2)
            {
                UseButton.SetActive(false);
                NoticeText.text = "아이템을 사용할 펫을 선택해주세요\n동행한 펫에게만 사용 가능";
            }
            else
            {
                UseButton.SetActive(true);
                NoticeText.text = "";
            }
        }
        else if (num == 14)
        {
            UseButton.SetActive(true);
            NoticeText.text = "";
            ItemState.IsUsingPetExpItem = true;
        }
        else
        {
            UseButton.SetActive(false);

            if (num == 2)
                NoticeText.text = "대전에서만 사용 가능";
            else if (num == 3)
                NoticeText.text = "던전 밖에서만 사용 가능";
            else if (num == 6)
                NoticeText.text = "병원에서만 사용 가능";
            else if (num == 7)
                NoticeText.text = "수련장에서만 사용 가능";
            else
                NoticeText.text = "펫 하우스에서만 사용 가능";
        }
    }

    public void ItemClick_Battle(int num)
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[1]);
        currentSelectItem = num;
        ItemInformtText.text = ItemState.ItemInforms[currentSelectItem].ToString();
        NoticeText.enabled = true;

        if ((num == 0) || (num == 1) || (num == 2) || (num == 4) || (num == 5))
        {
            NoticeText.text = "";
            if (ItemState.ItemCount[currentSelectItem] > 0)
                UseButton.SetActive(true);
            else
                UseButton.SetActive(false);
        }
        else if ((num == 8) || (num == 9) || (num == 10))
        {
            if ((currentSelectPet == 5) || (ItemState.ItemCount[currentSelectItem] == 0))
            {
                UseButton.SetActive(false);
                NoticeText.text = "";
                if (currentSelectPet == 5)
                    NoticeText.text = "아이템을 사용할 펫을 선택해주세요\n동행한 펫에게만 사용 가능";
            }
            else if (PetState.PetStates[currentSelectPet] != 2)
            {
                UseButton.SetActive(false);
                NoticeText.text = "아이템을 사용할 펫을 선택해주세요\n동행한 펫에게만 사용 가능";
            }
            else
            {
                UseButton.SetActive(true);
                NoticeText.text = "";
            }
        }
        else if (num == 14)
        {
            UseButton.SetActive(true);
            NoticeText.text = "";
            ItemState.IsUsingPetExpItem = true;
        }
        else
        {
            UseButton.SetActive(false);

            if (num == 3)
                NoticeText.text = "던전 밖에서만 사용 가능";
            else if (num == 6)
                NoticeText.text = "병원에서만 사용 가능";
            else if (num == 7)
                NoticeText.text = "수련장에서만 사용 가능";
            else
                NoticeText.text = "펫 하우스에서만 사용 가능";
        }
    }

    public void UseItemClick()
    {
        TempAudio.volume = SoundState.musicVolume;
        if (ItemState.ItemCount[currentSelectItem] > 0)
        {
            if (currentSelectItem == 0)
            {
                if (PlayerState.HP < PlayerState.MaxHP)
                {
                    PlayerState.HP = (((PlayerState.HP + 10) <= PlayerState.MaxHP) ? (PlayerState.HP + 10) : PlayerState.MaxHP);
                    ItemState.ItemCount[currentSelectItem]--;
                    TempAudio.PlayOneShot(clips[3]);
                }
            }
            else if (currentSelectItem == 1)
            {
                if (PlayerState.HP < PlayerState.MaxHP)
                {
                    PlayerState.HP = PlayerState.MaxHP;
                    ItemState.ItemCount[currentSelectItem]--;
                    TempAudio.PlayOneShot(clips[3]);
                }
            }
            else if (currentSelectItem == 2)
            {
                if (PlayerState.ExpRewardMultiple == 1)
                {
                    PlayerState.ExpRewardMultiple = 2;
                    ItemState.ItemCount[currentSelectItem]--;
                    TempAudio.PlayOneShot(clips[3]);
                }
                else
                    NoticeText.text = "한 대전에서 누적 사용 불가";
            }
            else if (currentSelectItem == 3)
            {
                PlayerState.CurrentQuest++;
                ItemState.ItemCount[currentSelectItem]--;
                TempAudio.PlayOneShot(clips[3]);

                if (PlayerState.CurrentQuest == PlayerState.TotalQuest[PlayerState.goal])
                {
                    GameObject.Find("Player").GetComponent<Player_Tps>().check = true;
                    GameObject.Find("Player").GetComponent<Player_Tps>().NextScene = "SuccessEnding";
                    Time.timeScale = 1f;
                }
            }
            else if (currentSelectItem == 4)
            {
                PlayerState.AttackPowerPlus++;
                ItemState.ItemCount[currentSelectItem]--;
                TempAudio.PlayOneShot(clips[3]);
            }
            else if (currentSelectItem == 5)
            {
                PlayerState.ShieldPowerMult *= 0.5f;
                PlayerState.ShieldPower *= 0.5f; 
                ItemState.ItemCount[currentSelectItem]--;
                TempAudio.PlayOneShot(clips[3]);
            }
            else if (currentSelectItem == 8)
            {
                if (PetState.Health[currentSelectPet] < 10)
                {
                    PetState.Health[currentSelectPet] = ((PetState.Health[currentSelectPet]++ <= 10) ? PetState.Health[currentSelectPet]++ : 10);
                    ItemState.ItemCount[currentSelectItem]--;
                    ShowPetHealthBar();
                    TempAudio.PlayOneShot(clips[4]);
                }
            }
            else if (currentSelectItem == 9)
            {
                if (PetState.Health[currentSelectPet] < 10)
                {
                    PetState.Health[currentSelectPet] = (((PetState.Health[currentSelectPet] + 3) <= 10) ? (PetState.Health[currentSelectPet] + 3) : 10);
                    ItemState.ItemCount[currentSelectItem]--;
                    ShowPetHealthBar();
                    TempAudio.PlayOneShot(clips[4]);
                }
            }
            else if (currentSelectItem == 10)
            {
                if (PetState.Health[currentSelectPet] < 10)
                {
                    PetState.Health[currentSelectPet] = 10;
                    ItemState.ItemCount[currentSelectItem]--;
                    ShowPetHealthBar();
                    TempAudio.PlayOneShot(clips[4]);
                }
            }
            else if (currentSelectItem == 14)
            {
                if (PlayerState.PetExpRewardMultiple == 1.0f)
                {
                    PlayerState.PetExpRewardMultiple = 1.5f;
                    ItemState.ItemCount[currentSelectItem]--;
                    TempAudio.PlayOneShot(clips[4]);
                }
            }
            ItemCountText[currentSelectItem].text = ItemState.ItemCount[currentSelectItem].ToString();
        }
        ItemClick(currentSelectItem);
    }

    private void ShowPetHealthBar()
    {
        localScale_PetHealth.x = PetState.Health[currentSelectPet] / (float)4.0;
        pet_HealthBar.transform.localScale = localScale_PetHealth;
    }

    public void Click_ItemSelectButton()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[0]);
        ItemSelectButtonText.color = Color.yellow;
        PetItemSelectButtonText.color = Color.white;
        m_PetItemSet.SetActive(false);
        m_ItemSet.SetActive(true);
        ItemInformtText.text = "";
        NoticeText.text = "";
        UseButton.SetActive(false);
        PetSelected.SetActive(false);
        pet_HealthBarSet.SetActive(false);
        currentSelectPet = 5;
    }

    public void Click_PetItemSelectButton()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[0]);
        ItemSelectButtonText.color = Color.white;
        PetItemSelectButtonText.color = Color.yellow;
        m_ItemSet.SetActive(false);
        m_PetItemSet.SetActive(true);
        ItemInformtText.text = "";
        NoticeText.text = "";
        UseButton.SetActive(false);
    }
    public void Click_XButton()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[1]);
        BagCanvas.SetActive(false);
    }
}
