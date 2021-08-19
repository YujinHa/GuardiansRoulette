using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuelSkillButton : MonoBehaviour
{
    public Image[] SkillButtonImages;
    public Image[] SkillButtons;
    public Image[] SkillCollTimeImages;
    public Text[] SkillCollTimes;
    private DuelSet m_DuelSet;
    public GameObject fireParticlePrefab;//3단계
    public GameObject iceParticlePrefab;//3단계
    public GameObject fireSkillParticlePrefab;//2단계
    public GameObject iceSkillParticlePrefab;//2단계
    public int MapPositionXMin;
    public int MapPositionXMax;
    public float MapPositionYMin;
    public float MapPositionYMax;
    public int MapPositionZMin;
    public int MapPositionZMax;

    void Start()
    {
        m_DuelSet = GameObject.Find("DuelManager").GetComponent<DuelSet>();

        for (int i = 0; i < 9; i++)
        {
            if (PlayerState.GotSkill[i] == true)
            {
                SkillButtonImages[i].color = Color.white;
                SkillButtons[i].color = Color.white;
            }
            else
            {
                SkillButtonImages[i].color = new Color(79 / 255f, 79 / 255f, 79 / 255f);
                SkillButtons[i].color = new Color(79 / 255f, 79 / 255f, 79 / 255f);
            }
            SkillCollTimeImages[i].enabled = false;
        }
    }

    
    void Update()
    {
        for (int i = 0; i < 9; i++)
        {
            if(SkillCollTimeImages[i].enabled == true)
                SkillCollTimes[i].text = ((int)PlayerState.SkillCollTime[i]).ToString();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            ClickSkillButtons(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            ClickSkillButtons(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            ClickSkillButtons(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            ClickSkillButtons(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
            ClickSkillButtons(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
            ClickSkillButtons(5);
        else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
            ClickSkillButtons(6);
        else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
            ClickSkillButtons(7);
        else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
            ClickSkillButtons(8);

    }

    public void SetCoolTimeImage(int index)//쿨타임 끝나면 쿨타임 글씨 없애기
    {
        SkillCollTimeImages[index].enabled = false;
        SkillCollTimes[index].text = "";
    }

    public void ClickSkillButtons(int index)
    {
        if((PlayerState.GotSkill[index] == true) && (PlayerState.SkillCollTime[index] <= 0.0f))
        {
            SkillCollTimeImages[index].enabled = true;
            float tempMinus = 0.0f;
            
            if(PetState.PetStates[1] == 2)
            {
                if (PetState.Lv[1] == 0)
                    tempMinus = 2.0f;
                else if (PetState.Lv[1] == 1)
                    tempMinus = 4.0f;
                else if (PetState.Lv[1] == 2)
                    tempMinus = 6.0f;
            }
            PlayerState.SkillCollTime[index] = 30.0f - tempMinus;

            if(index == 0)
            {
                m_DuelSet.GetComponent<AudioSource>().volume = SoundState.musicVolume;
                m_DuelSet.GetComponent<AudioSource>().PlayOneShot(m_DuelSet.GetComponent<DuelSet>().Clips[1]);
                GameObject.Find("Launcher").GetComponent<Player_Duel>().AttackMode = 1;
                StartCoroutine("WaitForSkillFinish_5");
            }
            else if (index == 1)
            {
                m_DuelSet.GetComponent<AudioSource>().volume = SoundState.musicVolume;
                m_DuelSet.GetComponent<AudioSource>().PlayOneShot(m_DuelSet.GetComponent<DuelSet>().Clips[4]);
                MakeParticle_Skill2(fireSkillParticlePrefab);
            }
            else if (index == 2)
            {
                m_DuelSet.GetComponent<AudioSource>().volume = SoundState.musicVolume;
                m_DuelSet.GetComponent<AudioSource>().PlayOneShot(m_DuelSet.GetComponent<DuelSet>().Clips[7]);
                for (int i = 0; i < m_DuelSet.EnemyNum; i++)
                {
                    m_DuelSet.EnemyHP[i] = ((m_DuelSet.EnemyHP[i] - 15 > 0 ) ? m_DuelSet.EnemyHP[i] - 15 : 0);
                    if (GameObject.Find("Enemy" + i))
                    {
                        Animator Anim = GameObject.Find("Enemy" + i).GetComponent<Animator>();
                        Anim.SetTrigger("IsDamaged");

                        if (m_DuelSet.EnemyHP[i] <= 0)
                        {
                            m_DuelSet.EnemyHPIcon[i].color = Color.black;
                            Anim.SetTrigger("IsDie");

                            GameObject.Find("Enemy" + i).GetComponent<EnemyAI>().EnemyDie();
                        }
                        
                        GameObject tempParticle = Instantiate(fireParticlePrefab, transform.position, transform.rotation) as GameObject;
                        tempParticle.transform.position = GameObject.Find("Enemy" + i).GetComponent<Transform>().position;
                    }
                }
            }
            else if (index == 3)
            {
                m_DuelSet.GetComponent<AudioSource>().volume = SoundState.musicVolume;
                m_DuelSet.GetComponent<AudioSource>().PlayOneShot(m_DuelSet.GetComponent<DuelSet>().Clips[5]);
                GameObject.Find("Launcher").GetComponent<Player_Duel>().AttackMode = 2;
                StartCoroutine("WaitForSkillFinish_5");
            }
            else if (index == 4)
            {
                m_DuelSet.GetComponent<AudioSource>().volume = SoundState.musicVolume;
                m_DuelSet.GetComponent<AudioSource>().PlayOneShot(m_DuelSet.GetComponent<DuelSet>().Clips[5]);
                MakeParticle_Skill2(iceSkillParticlePrefab);
            }
            else if (index == 5)
            {
                m_DuelSet.GetComponent<AudioSource>().volume = SoundState.musicVolume;
                m_DuelSet.GetComponent<AudioSource>().PlayOneShot(m_DuelSet.GetComponent<DuelSet>().Clips[8]);
                for (int i = 0; i < m_DuelSet.EnemyNum; i++)
                {
                    m_DuelSet.EnemyHP[i] = ((m_DuelSet.EnemyHP[i] - 15 > 0) ? m_DuelSet.EnemyHP[i] - 15 : 0);

                    if (GameObject.Find("Enemy" + i))
                    {
                        Animator Anim = GameObject.Find("Enemy" + i).GetComponent<Animator>();
                        Anim.SetTrigger("IsDamaged");

                        if (m_DuelSet.EnemyHP[i] <= 0)
                        {
                            m_DuelSet.EnemyHPIcon[i].color = Color.black;
                            Anim.SetTrigger("IsDie");

                            GameObject.Find("Enemy" + i).GetComponent<EnemyAI>().EnemyDie();
                        }

                        GameObject tempParticle = Instantiate(iceParticlePrefab, transform.position, transform.rotation) as GameObject;
                        tempParticle.transform.position = GameObject.Find("Enemy" + i).GetComponent<Transform>().position;
                    }
                }
            }
            else if (index == 6)
            {
                m_DuelSet.GetComponent<AudioSource>().volume = SoundState.musicVolume;
                m_DuelSet.GetComponent<AudioSource>().PlayOneShot(m_DuelSet.GetComponent<DuelSet>().Clips[2]);
                m_DuelSet.IsPlayerShield = true;
                StartCoroutine("WaitForShieldFinish_5");
            }
            else if (index == 7)
            {
                m_DuelSet.GetComponent<AudioSource>().volume = SoundState.musicVolume;
                m_DuelSet.GetComponent<AudioSource>().PlayOneShot(m_DuelSet.GetComponent<DuelSet>().Clips[3]);
                Physics.IgnoreLayerCollision(8, 0, true);
                Physics.IgnoreLayerCollision(8, 10, true);
                Physics.IgnoreLayerCollision(8, 11, true);
                
                m_DuelSet.IsPlayerShield = true;
                m_DuelSet.IsTransparent = true;
                StartCoroutine("WaitForShieldFinish_8");
            }
            else if (index == 8)
            {
                m_DuelSet.GetComponent<AudioSource>().volume = SoundState.musicVolume;
                m_DuelSet.GetComponent<AudioSource>().PlayOneShot(m_DuelSet.GetComponent<DuelSet>().Clips[6]);
                m_DuelSet.IsPlayerShield = true;
                m_DuelSet.IsReverseAttack = true;
                StartCoroutine("WaitForShieldFinish_10");
            }
        }
    }

    void MakeParticle_Skill2(GameObject SkillParticlePrefab)
    {
        for (int i = 0; i < 80; i++)
        {
            float tempRandX = Random.Range(0, MapPositionXMax - MapPositionXMin) + MapPositionXMin;
            float tempRandY = Random.Range(0, MapPositionYMax - MapPositionYMin) + MapPositionYMin;
            float tempRandZ = Random.Range(0, MapPositionZMax - MapPositionZMin) + MapPositionZMin;
            
            GameObject tempParticle = Instantiate(SkillParticlePrefab, new Vector3(tempRandX, tempRandY, tempRandZ), Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f))) as GameObject;
        }
    }

    IEnumerator WaitForSkillFinish_5()
    {
        yield return new WaitForSeconds(5.0f);
        GameObject.Find("Launcher").GetComponent<Player_Duel>().AttackMode = 0;
    }

    IEnumerator WaitForShieldFinish_5()
    {
        yield return new WaitForSeconds(5.0f);
        m_DuelSet.IsPlayerShield = false;
    }

    IEnumerator WaitForShieldFinish_8()
    {
        yield return new WaitForSeconds(8.0f);
        m_DuelSet.IsTransparent = false;
        m_DuelSet.IsPlayerShield = false;
        Physics.IgnoreLayerCollision(8, 0, false);
        Physics.IgnoreLayerCollision(8, 10, false);
        Physics.IgnoreLayerCollision(8, 11, false);
    }

    IEnumerator WaitForShieldFinish_10()
    {
        yield return new WaitForSeconds(10.0f);
        m_DuelSet.IsReverseAttack = false;
        m_DuelSet.IsPlayerShield = false;
    }
}
