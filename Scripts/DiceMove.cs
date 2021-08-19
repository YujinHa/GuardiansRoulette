using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceMove : MonoBehaviour
{
    public GameObject Dice;
    public GameObject Player;
    private Dice_Highlight m_Dice_Highlight;
    public GameObject m_DiceHighlight;
    public bool Selected = false;

    void Start()
    {
        //transform.position = GameObject.Find("Player").transform.position + new Vector3(0, 5f, -1.8f);
        m_Dice_Highlight = m_DiceHighlight.GetComponent<Dice_Highlight>();
    }


    void Update()
    {

    }

    void FixedUpdate()
    {
        //주사위 선택 안됐으면 랜덤으로 굴리고 선택되면 대기 후 삭제하고 이동하는 함수 호출
        if (Selected == false)
            transform.Rotate(new Vector3(Random.Range(1, 100), Random.Range(1, 100), Random.Range(1, 100)), Space.Self);
        else if (Player.GetComponent<BoardPlayer>().Checked == false)
        {
            Player.GetComponent<BoardPlayer>().DiceSelected = true;
            Player.GetComponent<BoardPlayer>().Checked = true;
            Invoke("DeleteAndMove", 2f);
        }
    }

    //주사위 없애고 플레이어 다음 칸으로 이동
    private void DeleteAndMove()
    {
        Player.GetComponent<BoardPlayer>().JumpMove();
        m_Dice_Highlight.SetHighlight();
        Dice.SetActive(false);
    }

    private void GetSelectNum()
    {

    }
}
