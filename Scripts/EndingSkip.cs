using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingSkip : MonoBehaviour
{
    static public bool IsSuccess = false;
    public Text GoalName;
    public Text HP;
    public Text LV;
    public Text Quest;
    public Text PetNum;
    public Image Goalmage;
    public Sprite[] Goalmages;

    public GameObject FadeOut;
    float finishtime = 0f;
    bool check = false;
    private string NextScene;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        FadeOut.SetActive(false);
        HP.text = ((int)PlayerState.HP).ToString() + "/100";
        LV.text = (PlayerState.ExpLv + 1).ToString();
        Quest.text = PlayerState.CurrentQuest.ToString() + "/" + PlayerState.TotalQuest[PlayerState.goal].ToString();
        PetNum.text = PetState.GotPetNum.ToString();

        if (IsSuccess == true)
        {
            GoalName.text = PlayerState.Name[PlayerState.goal];
            Goalmage.enabled = true;
            Goalmage.sprite = Goalmages[PlayerState.goal];
        }
        else
        {
            GoalName.text = "수련 결과";
            Goalmage.enabled = false;
        }
    }

    void Update()
    {
        if (check == true)
            finishtime = finishtime + Time.deltaTime;

        if (finishtime >= 1.0f)
            Loading.loading(NextScene);
    }

    public void Click_EndingSkip()
    {
        check = true;
        FadeOut.SetActive(true);
        NextScene = "Menu";
    }
}
