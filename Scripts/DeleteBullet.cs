using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBullet : MonoBehaviour
{
    public float removeTime = 2.0f;
    private DuelSet m_DuelSet;

    void Start()
    {
        m_DuelSet = GameObject.Find("DuelManager").GetComponent<DuelSet>();
        Destroy(gameObject, removeTime);
    }


    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            int tempIndex = collision.gameObject.transform.parent.parent.parent.GetComponent<EnemyAI>().Index;

            Animator Anim = collision.gameObject.transform.parent.parent.parent.GetComponent<Animator>();

            if (m_DuelSet.EnemyHP[tempIndex] > 0)
            {
                if (gameObject.tag == "FallSkill")
                {
                    m_DuelSet.EnemyHP[tempIndex] = ((m_DuelSet.EnemyHP[tempIndex] - 8) > 0 ? (m_DuelSet.EnemyHP[tempIndex] - 8) : 0);
                }
                else
                {
                    m_DuelSet.EnemyHP[tempIndex] = ((m_DuelSet.EnemyHP[tempIndex] - PlayerState.AttackPower) > 0 ?
                        (m_DuelSet.EnemyHP[tempIndex] - PlayerState.AttackPower) : 0);
                }
                Anim.SetTrigger("IsDamaged");
            }

            if(m_DuelSet.EnemyHP[tempIndex] <= 0)
            {
                m_DuelSet.EnemyHPIcon[tempIndex].color = Color.black;
                Anim.SetTrigger("IsDie");

                collision.gameObject.transform.parent.parent.parent.GetComponent<EnemyAI>().EnemyDie();
            }

            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
