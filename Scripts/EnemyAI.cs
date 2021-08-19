using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent m_enemy = null;
    private DuelSet m_DuelSet;
    public Transform[] m_WayPoints;
    int m_count = 0;
    public GameObject ParticleObject;
    public GameObject EnemyModel;

    Transform m_target = null;

    public int Index;
    
    public AudioSource TempAudio;
    public AudioClip Clip;

    public void SetTarget(Transform p_target)
    {
        CancelInvoke();
        m_target = p_target;
    }

    public void RemoveTarget()
    {
        m_target = null;
        InvokeRepeating("MoveToNextWayPoint", 0f, 2f);
    }

    void MoveToNextWayPoint()
    {
        if (m_target == null)
        {
            if (m_enemy.velocity == Vector3.zero)
            {
                m_count = Random.Range(0, m_WayPoints.Length);
                m_enemy.SetDestination(m_WayPoints[m_count].position);
            }
        }
    }

    void Start()
    {
        ParticleObject.SetActive(false);
        m_enemy = GetComponent<NavMeshAgent>();
        m_enemy.stoppingDistance = 3;

        m_DuelSet = GameObject.Find("DuelManager").GetComponent<DuelSet>();
        m_WayPoints = new Transform[m_DuelSet.WayPoints.Length];

        for (int i = 0; i < m_DuelSet.WayPoints.Length; i++)
            m_WayPoints[i] = m_DuelSet.WayPoints[i];

        float tempRand = Random.Range(0.0f, 3.0f);
        InvokeRepeating("MoveToNextWayPoint", 0f, tempRand);
    }

    void Update()
    {
        if (m_target != null)
        {
            m_enemy.SetDestination(m_target.position);
        }
    }

    public void EnemyDie()
    {
        ParticleObject.SetActive(true);
        ParticleObject.GetComponent<ParticleSystem>().Play();
        StartCoroutine("EnemyDieCoroutine");
    }

    IEnumerator EnemyDieCoroutine()
    {
        gameObject.GetComponent<EnemyView>().IsDie = true;
        EnemyModel.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        m_DuelSet.ClearQuestCheck();

        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(Clip);
        Destroy(gameObject);
    }
}
