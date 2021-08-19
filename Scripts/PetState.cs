using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetState : MonoBehaviour
{
    private static PetState p_Instance = null;

    public const int MaxHatchValue = 10;
    public const int MinHealth = 0;
    public const int MaxHealth = 10;
    public const int MaxExp = 10;
    public const int MinExp = 0;
    public const int MaxLv = 3;
    public const int TotalPetNum = 5;

    static public int AbleTotalPetNum = 1;//동행 가능한 펫 수
    static public int PetNum = 0;//동행하는 펫 수
    static public int GotPetNum = 0;//얻은 펫 수
    static public int[] Health = { 10, 10, 10, 10, 10 };
    static public float[] Exp = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    static public int[] Lv = { 0, 0, 0, 0, 0 };
    static public bool IsFullPet = false;//펫을 모두 보유했을 경우 true

    //PetStates가 0이면 팻을 아직 보유하지 않음, 1이면 보유하지만 동행X, 2이면 동행상태
    static public int HatchValue = 0;
    static public int[] PetStates = { 0, 0, 0, 0, 0 };

    private void Awake()
    {
        if (p_Instance)
        {
            DestroyImmediate(this.gameObject);
            return;
        }

        p_Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {

    }

    static public void SetStart()
    {
         AbleTotalPetNum = 1;
         PetNum = 0;
         GotPetNum = 0;
         IsFullPet = false;
        
        HatchValue = 0;
        for (int i = 0; i < 5; i++)
        {
            Health[i] = 10;
            Exp[i] = 0.0f;
            Lv[i] = 0;
            PetStates[i] = 0;
        }
    }

   static public void SetPetExp(float AddExp)
    {
        for (int i = 0; i < PetStates.Length; i++)
        {
            if (PetStates[i] == 2)
                Exp[i] += AddExp;

            CheckLv(i);
        }
    }

    //Exp갱신됐을 때 Exp확인해서 Lv변경
    static public void CheckLv(int index)
    {
        if (Exp[index] >= MaxExp)
        {
            Lv[index]++;
            Exp[index] = Exp[index] - MaxExp;
        }
    }

}
