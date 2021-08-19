using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DuelSet : MonoBehaviour
{
    public int EnemyNum;
    public float[] EnemyMaxHP;
    public float[] EnemyHP;
    static public float RewardPlayerExp;
    static public int RewardCoin;
    public int EnemyAttackPower = 1;
    public float RewardPetExp = 1;

    public bool IsPlayerShield = false;
    public bool IsReverseAttack = false;
    public bool IsTransparent = false;

    public GameObject QuestClearImage;
    public GameObject QuestClearParticle;
    public Text RewardText;
    public GameObject aimImage;

    public GameObject[] EnemyHPBarSets;
    public GameObject[] EnemyHPBars;
    public Image[] EnemyHPIcon;
    private Vector3[] localScale_EnemyHP;
    public GameObject[] Enemys;
    public Transform[] WayPoints;
    private bool[] WayPointsCheck;

    public GameObject FadeOut;
    float finishtime = 0f;
    bool check = false;
    private string NextScene;
    public AudioSource TempAudio;
    public AudioClip[] Clips;
    private bool checkClear = false;
    public bool IsFinished = false;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.Find("SoundState").GetComponent<AudioSource>().clip = GameObject.Find("SoundState").GetComponent<SoundState>().audioClip[15];
        GameObject.Find("SoundState").GetComponent<AudioSource>().Play();
        FadeOut.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
       
        aimImage.SetActive(true);
        QuestClearImage.SetActive(false);
        QuestClearParticle.SetActive(false);
        EnemyAttackPower = 1;
        RewardPetExp = 1;
        PlayerState.mapStates = 2;
        PlayerState.ExpRewardMultiple = 1;

        //적 개수 랜덤 셋팅
        float randNum = Random.Range(0, 2);
        if (randNum == 0 )
            EnemyNum = (PlayerState.CurrentQuest - 1) / 5 + 2;
        else if (randNum == 1)
            EnemyNum = (PlayerState.CurrentQuest - 1) / 5 + 3;

        EnemyHP = new float[EnemyNum];
        EnemyMaxHP = new float[EnemyNum];
        localScale_EnemyHP = new Vector3[EnemyNum];
        RewardPlayerExp = EnemyNum;
        RewardCoin = (PlayerState.CurrentQuest - 1) / 5 + 1;

        WayPointsCheck = new bool[WayPoints.Length];
        for (int i = 0; i < WayPoints.Length; i++)
            WayPointsCheck[i] = false;

        //적 HP 랜덤 셋팅
        for (int i = 0; i < EnemyNum; i++)
        {
            if ((PlayerState.CurrentQuest - 1) / 5 == 0)
                EnemyHP[i] = 10.0f;
            else if (((PlayerState.CurrentQuest - 1) / 5 == 1) || ((PlayerState.CurrentQuest - 1) / 5 == 2))
            {
                float temprandNum = Random.Range(0.0f, 1.0f);
                if (temprandNum < 0.5)
                    EnemyHP[i] = (PlayerState.CurrentQuest - 1) / 5 * 10;
                else if (temprandNum >= 0.5)
                    EnemyHP[i] = (PlayerState.CurrentQuest - 1) / 5 * 10 + 10;
            }
            else if ((PlayerState.CurrentQuest - 1) / 5 == 3)
            {
                float temprandNum = Random.Range(0.0f, 1.0f);
                if (temprandNum < 0.3)
                    EnemyHP[i] = (PlayerState.CurrentQuest - 1) / 5 * 10 - 10;
                else if (temprandNum < 0.7)
                    EnemyHP[i] = (PlayerState.CurrentQuest - 1) / 5 * 10;
                else
                    EnemyHP[i] = (PlayerState.CurrentQuest - 1) / 5 * 10 + 10;
            }
            else if ((PlayerState.CurrentQuest - 1) / 5 == 4)
            {
                float temprandNum = Random.Range(0.0f, 1.0f);
                if (temprandNum < 0.5)
                    EnemyHP[i] = (PlayerState.CurrentQuest - 1) / 5 * 10 - 10;
                else if (temprandNum >= 0.5)
                    EnemyHP[i] = (PlayerState.CurrentQuest - 1) / 5 * 10;
            }
            else
                EnemyHP[i] = 40.0f;

            EnemyMaxHP[i] = EnemyHP[i];

            GameObject Enemy = Instantiate(Enemys[(int)EnemyHP[i] / 10 - 1]) as GameObject;
            Enemy.GetComponent<EnemyAI>().Index = i;
            Enemy.GetComponent<EnemyView>().Index = i;
            Enemy.name = "Enemy" + i.ToString();

            while (true)
            {
                int tempRand = Random.Range(0, WayPoints.Length);
                if (WayPointsCheck[tempRand] == false)
                {
                    Enemy.transform.position = WayPoints[tempRand].position;
                    WayPointsCheck[tempRand] = true;
                    break;
                }
            }
        }

        for (int i = 0; i < 8; i++)
        {
            if (i < EnemyNum)
            {
                EnemyHPBarSets[i].SetActive(true);
                localScale_EnemyHP[i] = EnemyHPBars[i].transform.localScale;
                if (EnemyMaxHP[i] == 10.0f)
                {
                    EnemyHPIcon[i].color = new Color(252 / 255f, 255 / 255f, 124 / 255f);
                    EnemyHPBars[i].GetComponent<Image>().color = new Color(252 / 255f, 255 / 255f, 124 / 255f);
                }
                else if (EnemyMaxHP[i] == 20.0f)
                {
                    EnemyHPIcon[i].color = new Color(0 / 255f, 184 / 255f, 255 / 255f);
                    EnemyHPBars[i].GetComponent<Image>().color = new Color(0 / 255f, 184 / 255f, 255 / 255f);
                }
                else if (EnemyMaxHP[i] == 30.0f)
                { 
                    EnemyHPIcon[i].color = new Color(198 / 255f, 91 / 255f, 255 / 255f);
                    EnemyHPBars[i].GetComponent<Image>().color = new Color(198 / 255f, 91 / 255f, 255 / 255f);
                }
                else if (EnemyMaxHP[i] == 40.0f)
                { 
                    EnemyHPIcon[i].color = new Color(255 / 255f, 65 / 255f, 57 / 255f);
                    EnemyHPBars[i].GetComponent<Image>().color = new Color(255 / 255f, 65 / 255f, 57 / 255f);
                }
            }
            else
                EnemyHPBarSets[i].SetActive(false);
        }

        if (PetState.PetStates[0] == 2)
        {
            if (PetState.Lv[0] == 0)
                PlayerState.AttackPower = 2f;
            else if (PetState.Lv[0] == 1)
                PlayerState.AttackPower = 2.5f;
            else if (PetState.Lv[0] == 2)
                PlayerState.AttackPower = 3f;
        }
        else
            PlayerState.AttackPower = 1f;

        PlayerState.AttackPower = PlayerState.AttackPower + PlayerState.AttackPowerPlus;

        if (PetState.PetStates[3] == 2)
        {
            if (PetState.Lv[3] == 0)
                PlayerState.ShieldPower = 0.9f;
            else if (PetState.Lv[3] == 1)
                PlayerState.ShieldPower = 0.8f;
            else if (PetState.Lv[3] == 2)
                PlayerState.ShieldPower = 0.7f;
        }
        else
            PlayerState.ShieldPower = 1f;

        PlayerState.ShieldPower = PlayerState.ShieldPower * PlayerState.ShieldPowerMult;
    }
    
    void Update()
    {
        EnemyHPBarUpdate();
        if (check == true)
            finishtime = finishtime + Time.deltaTime;

        if (finishtime >= 1.0f)
            Loading.loading(NextScene);
    }
    
    public void EnemyHPBarUpdate()
    {
        for (int i = 0; i < EnemyNum; i++)
        {
            localScale_EnemyHP[i].x = EnemyHP[i] / EnemyMaxHP[i] * 2.5f;
            EnemyHPBars[i].transform.localScale = localScale_EnemyHP[i];
        }
    }

    public void ClearQuestCheck()
    {
        for (int i = 0; i < EnemyNum; i++)
        {
            if (EnemyHP[i] > 0)
                return;
        }
        if (!checkClear)
        {
            checkClear = true;

            GameObject.Find("SoundState").GetComponent<AudioSource>().Stop();
            TempAudio.volume = SoundState.musicVolume;
            TempAudio.PlayOneShot(Clips[0]);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameObject.Find("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().InputOK = false;
            aimImage.SetActive(false);
            QuestClearImage.SetActive(true);
            QuestClearParticle.SetActive(true);
            QuestClearParticle.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
            QuestClearParticle.transform.rotation = Camera.main.transform.rotation;

            if (ItemState.IsUsingPetExpItem)
                RewardPetExp = 1.5f;

            int rewardCoin = (PlayerState.CurrentQuest - 1) / 5 + 1;

            if (PetState.PetStates[2] == 2)
            {
                if (PetState.Lv[2] == 0)
                    rewardCoin += 1;
                else if (PetState.Lv[2] == 1)
                    rewardCoin += 2;
                else if (PetState.Lv[2] == 2)
                    rewardCoin += 3;
            }

            RewardText.text = EnemyNum.ToString() + " Exp\n" + RewardPetExp.ToString() + " Exp\n" + rewardCoin.ToString() + " 골드";

            PlayerState.Coin += rewardCoin;

            float tempExp = EnemyNum * PlayerState.ExpRewardMultiple;

            if (PlayerState.Exp + tempExp > PlayerState.MaxExp[PlayerState.ExpLv])
            {
                PlayerState.Exp = PlayerState.Exp + tempExp - PlayerState.MaxExp[PlayerState.ExpLv];
                PlayerState.ExpLv++;
            }
            else
                PlayerState.Exp = PlayerState.Exp + tempExp;

            PetState.SetPetExp(RewardPetExp);

            for (int i = 0; i < 5; i++)
            {
                if (PetState.PetStates[i] == 2)
                {
                    PetState.Health[i] -= 1;

                    if (PetState.Health[i] <= 0)
                    {
                        PetState.PetStates[i] = 1;
                        PetState.PetNum--;
                    }
                }
            }
        }
    }

    public void Click_NextButton_ClearQuest()
    {
        check = true;
        FadeOut.SetActive(true);
        NextScene = "Dongeon";
    }
}
