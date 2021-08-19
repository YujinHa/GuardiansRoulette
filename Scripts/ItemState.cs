using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemState : MonoBehaviour
{
    private static ItemState I_Instance = null;

    static public int[] ItemCount = new int[16];
    static public int[] ItemPrice = { 10, 80, 4, 10, 4, 4, 3, 3, 1, 3, 7, 1, 5, 10, 10, 20 };
    static public bool IsUsingPetExpItem = false;//퀘스트 끝날 때 초기화 해주기
    //public Image Item_HP;

    static public string[] ItemInforms = {
        "HP 증가 아이템\n\n효과\n플레이어 HP +10",
        "HP 완전회복 아이템\n\n효과\n플레이어 HP 최대치로 회복",
        "경험치 증가 아이템\n\n효과\n대전에서 이겼을 때 보상으로 받는\n\n경험치 2배 증가\n\n효력: 대전 한 판\n누적 사용 불가",
        "퀘스트 프리패스 아이템\n\n효과\n퀘스트 하나 통과\n\n퀘스트에 대한 경험치나 보상은 받을 수 없음",
        "공격력 증가 아이템\n\n효과\n플레이어 공격력 +1\n\n효력: 한 퀘스트\n누적 사용 가능",
        "방어력 증가 아이템\n\n효과\n플레이어가 받는 데미지 50%감소\n\n효력: 한 퀘스트\n누적 사용 가능",
        "병원 회복력 증가 아이템\n\n효과\n병원 미니게임 성적의 1.5배로 회복\n\n병원에서만 사용 가능",
        "수련장 버프 아이템\n\n효과\n수련장 미니게임 시 플레이어에게 유리하게\n제한 시간 감소/증가 또는 목표수 감소\n\n수련장에서만 사용 가능",
    "곰젤리\n\n효과\n펫 체력 +1\n\n펫에게만 사용 가능",
    "고양이쿠키\n\n효과\n펫 체력 +3\n\n펫에게만 사용 가능",
    "아이스크림\n\n효과\n펫 체력 최대치로 회복\n\n펫에게만 사용 가능",
    "마포\n\n효과\n알 성장 +1\n\n알에만 사용 가능",
    "실\n\n효과\n알 성장 +5\n\n알에만 사용 가능",
    "담요\n\n효과\n알 성장 최대치 증가\n사용 즉시 펫으로 부화 가능\n\n알에만 사용 가능",
    "펫 경험치 증가 아이템\n\n효과\n대전에 동행하는 펫이 획득하는\n경험치 1.5배 증가\n\n이 아이템을 사용하는\n퀘스트 동안의 보상에만 효력 작용\n중복 적용 불가",
    "전체 동행 펫 수 증가 아이템\n\n효과\n플레이어와 함께 던전에\n동행할 수 있는 펫 최대수 증가\n\n누적 사용 가능"
};

    private void Awake()
    {
        if (I_Instance)
        {
            DestroyImmediate(this.gameObject);
            return;
        }

        I_Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        IsUsingPetExpItem = false;
        for (int i = 0; i < 16; i++)
        {
            ItemCount[i] = 0;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public static void SetStart()
    {
        IsUsingPetExpItem = false;
        for (int i = 0; i < 16; i++)
        {
            ItemCount[i] = 0;
        }
    }

    void Update()
    {
        
    }
    
    public void ItemButtonClick()
    {
    }
}
