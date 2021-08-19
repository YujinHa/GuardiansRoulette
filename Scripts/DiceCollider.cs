using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceCollider : MonoBehaviour
{
    public Text text;
    public int Num;
    public GameObject DiceMove;
    private DiceMove Dice;
    public GameObject Player;
    private Dice_Highlight m_Dice_Highlight;
    public GameObject m_DiceHighlight;
    public GameObject DiceParticle;

    void Start()
    {
        Dice = DiceMove.GetComponent<DiceMove>();
        m_Dice_Highlight = m_DiceHighlight.GetComponent<Dice_Highlight>();
    }
    
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((Dice.Selected == false) && (collision.gameObject.tag == "Player"))
        {
            DiceParticle.SetActive(true);
            DiceParticle.transform.position = Camera.main.transform.position + Camera.main.transform.forward;
            text.text = Num.ToString();
            Player.GetComponent<BoardPlayer>().SelectNum = Num;
            Player.GetComponent<BoardPlayer>().DiceSelected = true;
            Dice.Selected = true;
            m_Dice_Highlight.HighlightSides(Num);
            StartCoroutine("WaitforParticle");
        }
    }

    IEnumerator WaitforParticle()
    {
        yield return new WaitForSeconds(1.5f);
        DiceParticle.SetActive(false);
    }
}
