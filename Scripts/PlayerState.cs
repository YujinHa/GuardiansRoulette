using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private static PlayerState s_Instance = null;
    //전체 씬이 공유하는 플레이어 상태

    static public bool ispaused = false;

    static public int goal = 0;//목표칭호 선택 창에서 할당해주기
    static public string[] Name = { "초급마법사", "중급마법사", "고급마법사" };//목표칭호 선택 창에서 할당해주기
    public const float MaxHP = 100;
    static public float HP = 100f;
    static public float[] MaxExp = { 100f, 200f, 300f };
    static public int ExpLv = 0;
    static public float Exp = 0f;
    static public int CurrentQuest = 0;
    static public int[] TotalQuest = { 10, 20, 30 };
    static public int Coin = 10;
    static public float coolTime_Hospital = 0f;
    static public bool UseHospitalItem = false;
    static public bool UseTrainingCenterItem = false;
    static public bool[] GotSkill = { false, false, false, false, false, false, false, false, false };
    static public float[] SkillCollTime = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};
    static public int mapStates = 0;//0: 마을 / 1: 던전 / 2: 대전 / 3: 상점, 병원, 수련장, 펫하우스
    static public int MiniGameChoice = 0;

    static public Vector3 VillagePlayerPosition = new Vector3(7.78f, -0.163f, -35.701f);
    static public Vector3 VillagePlayerRotation = new Vector3(0f, 0f, 0f);

    //던전용 변수
    static public int ExpRewardMultiple = 1;
    static public float AttackPower = 1;
    static public float AttackPowerPlus = 0;
    static public float ShieldPower = 1.0f;
    static public float ShieldPowerMult = 1.0f;
    static public float PetExpRewardMultiple = 1.0f;
    static public int CurrentStep;

    public static void SetStart()
    {
        HP = 100f;
        ispaused = false;
        ExpLv = 0;
        Exp = 0f;
        CurrentQuest = 0;
        Coin = 10;
        coolTime_Hospital = 0f;
        UseHospitalItem = false;
        UseTrainingCenterItem = false;
        mapStates = 0;
        MiniGameChoice = 0;
        VillagePlayerPosition = new Vector3(7.78f, -0.163f, -35.701f);
        VillagePlayerRotation = new Vector3(0f, 0f, 0f);

        ExpRewardMultiple = 1;
        AttackPower = 1;
        AttackPowerPlus = 0;
        ShieldPower = 1.0f;
        ShieldPowerMult = 1.0f;
        PetExpRewardMultiple = 1.0f;

        for(int i = 0; i < 9; i++)
        {
            SkillCollTime[i] = 0.0f;
            GotSkill[i] = false;
        }

        //
        //goal = 2;
        //HP = 70f;
        //ExpLv = 1;
        //Exp = 35f;
        //CurrentQuest = 23;
        //GotSkill[0] = true;
        //GotSkill[1] = true;
        //GotSkill[2] = true;
        //GotSkill[4] = true;
        //GotSkill[3] = true;
        //GotSkill[6] = true;
    }

    private void Awake()
    {
        if(s_Instance)
        {
            DestroyImmediate(this.gameObject);
            return;
        }

        s_Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        SetStart();
    }
    
    void Update()
    {
        if(coolTime_Hospital > 0)
            CoolTime_Hospital_Manager();

        if(mapStates == 2)
        {
            CoolTime_Duel();
        }
    }

    public void CoolTime_Hospital_Manager()
    {
        coolTime_Hospital -=  Time.deltaTime;
    }

    public void CoolTime_Duel()
    {
        if(mapStates == 2)
        {
            for (int i = 0; i < 9; i++)
            {
                if (SkillCollTime[i] > 0)
                {
                    SkillCollTime[i] -= Time.deltaTime;

                    if (SkillCollTime[i] <= 0)
                    {
                        if(GameObject.Find("SkillButtons"))
                            GameObject.Find("SkillButtons").GetComponent<DuelSkillButton>().SetCoolTimeImage(i);
                    }
                }
            }
        }
    }

    static public void SetVillagePlayerPosition(Vector3 position, Vector3 rotation)
    {
        VillagePlayerPosition = position;
        VillagePlayerRotation = rotation;
    }
}
