using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteEnemyBullet : MonoBehaviour
{
    private float removeTime = 2.0f;
    private DuelSet m_DuelSet;
    public int Index;

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
        if (!m_DuelSet.IsFinished)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (!m_DuelSet.IsPlayerShield)
                {
                    Animator Anim = GameObject.Find("MainPlayer").GetComponent<Animator>();

                    if (PlayerState.HP > 0)
                    {
                        PlayerState.HP = ((PlayerState.HP - m_DuelSet.EnemyAttackPower * PlayerState.ShieldPower) > 0 ?
                            (PlayerState.HP - m_DuelSet.EnemyAttackPower * PlayerState.ShieldPower) : 0);

                        Anim.SetTrigger("Hurt");
                    }

                    if (PlayerState.HP <= 0)
                    {
                        Debug.Log("Die");
                        Anim.SetTrigger("Fail");
                        EndingSkip.IsSuccess = false;
                        m_DuelSet.IsFinished = true;
                        GameObject.Find("Launcher").GetComponent<Player_Duel>().StartCoroutine("PlayerDie");
                    }
                }

                if (m_DuelSet.IsReverseAttack)
                {
                    Animator Anim = GameObject.Find("Enemy" + Index).GetComponent<Animator>();

                    Anim.SetTrigger("IsDamaged");

                    m_DuelSet.EnemyHP[Index] = (m_DuelSet.EnemyHP[Index] - m_DuelSet.EnemyAttackPower > 0 ? m_DuelSet.EnemyHP[Index] - m_DuelSet.EnemyAttackPower : 0);

                    if (m_DuelSet.EnemyHP[Index] <= 0)
                    {
                        m_DuelSet.EnemyHPIcon[Index].color = Color.black;
                        Anim.SetTrigger("IsDie");

                        GameObject.Find("Enemy" + Index).GetComponent<EnemyAI>().EnemyDie();
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}
