using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] float m_angle = 60f;
    [SerializeField] float m_distance = 10f;
    [SerializeField] LayerMask m_layeMask = 0;
    [SerializeField] EnemyAI m_enemy = null;

    static public bool IsAttacking = false;
    Animator Anim;
    public GameObject bulletPrefab;
    private float throwSpeed = 30f;
    private float checkTime = 0.0f;
    private float AttackTime = 1.133f;
    public bool IsDie = false;
    public int Index;
    private DuelSet m_DuelSet;
    public AudioSource TempAudio;
    public AudioClip Clip;

    private void Start()
    {
        m_DuelSet = GameObject.Find("DuelManager").GetComponent<DuelSet>();
        Anim = GetComponent<Animator>();

        if(PetState.PetStates[4] == 2)
        {
            if(PetState.Lv[4] == 0)
                AttackTime = 1.5f;
            else if (PetState.Lv[4] == 1)
                AttackTime = 2.0f;
            else if (PetState.Lv[4] == 2)
                AttackTime = 3.0f;
        }
    }

    void Sight()
    {
        Collider[] t_cols = Physics.OverlapSphere(transform.position, m_distance, m_layeMask);

        if (t_cols.Length > 0)
        {
            Transform t_tfPlayer = t_cols[0].transform;
            Vector3 t_direction = (t_tfPlayer.position - transform.position).normalized;
            float t_angle = Vector3.Angle(t_direction, transform.forward);

            if (t_angle < m_angle * 0.5f)
            {
                if ((Physics.Raycast(transform.position, t_direction, out RaycastHit t_hit, m_distance)) && (!m_DuelSet.IsTransparent))
                {
                    if (t_hit.transform.name == "Player" && (t_hit.distance > 3))
                    {
                        m_enemy.SetTarget(t_hit.transform);
                    }
                    if (t_hit.transform.name == "Player")
                        Attack();
                    else
                        IsAttacking = false;
                }
                else
                {
                    m_enemy.RemoveTarget();
                    IsAttacking = false;
                    Anim.SetBool("IsAttack", false);
                }
            }
        }
        else
        {
            m_enemy.RemoveTarget();
            IsAttacking = false;
            Anim.SetBool("IsAttack", false);
        }
    }
    
    void Attack()
    {
        if (checkTime > AttackTime)
        {
            TempAudio.volume = SoundState.musicVolume;
            TempAudio.PlayOneShot(Clip);
            IsAttacking = true;
            Anim.SetBool("IsAttack", true);

            GameObject newBullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation) as GameObject;
            newBullet.name = "Bullet";
            newBullet.GetComponent<DeleteEnemyBullet>().Index = Index;
            newBullet.GetComponent<Rigidbody>().velocity = transform.forward * throwSpeed;
            checkTime = 0.0f;
        }
    }
    
    void Update()
    {
        if ((!IsDie) && (!m_DuelSet.IsFinished))
        {
            checkTime += Time.deltaTime;

            Sight();
        }
        else if (m_DuelSet.IsFinished)
        {
            m_enemy.RemoveTarget();
            IsAttacking = false;
            Anim.SetBool("IsAttack", false);
        }
    }
}
