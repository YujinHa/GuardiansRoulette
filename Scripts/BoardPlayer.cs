using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoardPlayer : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    Animator m_anim;

    public int StepNum = 0;
    public int StepTotalNum;
    public GameObject[] Steps;
    private GameObject CurrentStep = null;
    public bool Checked = false;//점프해서 다음 칸에 닿았는지 여부
    public bool DiceSelected = false;
    private int StepAdd = 0;
    public GameObject Dice;
    public GameObject QuestClear;

    public GameObject[] PetPoints;
    public GameObject[] Pets;
    private int PointIndex = 0;
 //   private int Placenum = 0;

    public Text DiceNum;

    public float jumpPower = 5f;
    bool isJumping;

    public int SelectNum;

    public GameObject FadeOut;
    float finishtime = 0f;
    bool check = false;
    private string NextScene;
    public AudioSource TempAudio;
    public AudioClip[] Clips;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.Find("SoundState").GetComponent<AudioSource>().clip = GameObject.Find("SoundState").GetComponent<SoundState>().audioClip[12];
        GameObject.Find("SoundState").GetComponent<AudioSource>().Play();
        FadeOut.SetActive(false);
        PlayerState.mapStates = 1;
        m_anim = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        transform.position = Steps[PlayerState.CurrentStep].transform.position + new Vector3(0, 0.1f, 0);
        Dice.transform.position = GameObject.Find("DicePosition").transform.position;
        StepNum = PlayerState.CurrentStep;

        for (int i = 0; i < 5; i++)
        {
            if (PetState.PetStates[i] == 2)
            {
                Vector3 tempRotate = -(Camera.main.transform.forward);
                GameObject CurrentPet = Instantiate(Pets[i], PetPoints[PointIndex].transform.position, transform.rotation) as GameObject;
                CurrentPet.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
                CurrentPet.transform.localScale = new Vector3(3f, 3f, 3f);
                CurrentPet.name = "Pet" + PointIndex;
                PointIndex++;
            }
        }
    }
    
    void Update()
    {
        if (check == true)
            finishtime = finishtime + Time.deltaTime;

        if (finishtime >= 1.5f)
            Loading.loading(NextScene);

        if (DiceSelected == false)
        {
            if (Input.GetKey(KeyCode.Space))
                isJumping = true;
        }

        for(int i = 0; i < 5; i++)
        {
            if(GameObject.Find("Pet" + i))
                GameObject.Find("Pet" + i).GetComponent<Transform>().transform.position = PetPoints[i].transform.position;
        }
    }

    void FixedUpdate()
    {
        Jump();
    }

    void Jump()
    {
        if (!isJumping)
            return;
        m_Rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

        isJumping = false;
    }

    //포물선 이동 준비작업
    public void SetJump(Vector3 target)
    {
        Vector3 velocity = GetVelocity(transform.position, target, 45f);
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        SetVelocity(velocity);
    }

    //포물선 이동 속도준비작업
    public void SetVelocity(Vector3 velocity)
    {
        m_anim.SetTrigger("JumpTrigger");
        //m_anim.SetBool("Jump", true);
        m_Rigidbody.velocity = velocity;
    }

    //포물선 이동 구현
    public Vector3 GetVelocity(Vector3 currentPos, Vector3 targetPos, float initialAngle)
    {
        float gravity = Physics.gravity.magnitude;
        float angle = initialAngle * Mathf.Deg2Rad;

        Vector3 planarTarget = new Vector3(targetPos.x, 0, targetPos.z);
        Vector3 planarPosition = new Vector3(currentPos.x, 0, currentPos.z);

        float distance = Vector3.Distance(planarTarget, planarPosition);
        float yOffset = currentPos.y - targetPos.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPosition) * (targetPos.x > currentPos.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        return finalVelocity;
    }

    //플레이어가 칸에 닿았을 때 처리과정
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "EndStep")
        {
            PlayerState.SetVillagePlayerPosition(new Vector3(34.7f, -0.02f, 7.95f), new Vector3(0f, 280f, 0f));
            PlayerState.AttackPower = 1f;
            PlayerState.AttackPowerPlus = 0f;
            PlayerState.ExpRewardMultiple = 1;
            PlayerState.ShieldPower = 1.0f;
            PlayerState.ShieldPowerMult = 1.0f;
            PlayerState.PetExpRewardMultiple = 1.0f;
            ItemState.IsUsingPetExpItem = false;

            Dice.GetComponent<DiceMove>().Selected = false;
            DiceSelected = false;
            PlayerState.mapStates = 0;
            PlayerState.CurrentStep = 0;
            QuestClear.SetActive(true);
            GameObject.Find("SoundState").GetComponent<AudioSource>().Stop();
            TempAudio.volume = SoundState.musicVolume;
            TempAudio.PlayOneShot(Clips[6]);
            StartCoroutine("WaitAndFinshDongeon");
        }
        else
        {
            if (col.gameObject == CurrentStep)
            {
                StepAdd++;
                m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                Checked = false;
                if (StepAdd != SelectNum)
                    JumpMove();
                else
                {
                    CurrentStep = null;
                    StepAdd = 0;
                    StepAct(col);
                }
            }
            else if (col.gameObject.tag == "Step")
            {
                m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            }
        }
    }

    public void JumpMove()
    {
        DiceNum.text = "";
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(Clips[5]);
        Checked = true;
        StepNum++;
        CurrentStep = Steps[StepNum % Steps.Length];
        SetJump(new Vector3(CurrentStep.transform.position.x, CurrentStep.transform.position.y + 1f, CurrentStep.transform.position.z));
        
    }

    public void StepAct(Collision col)
    {
        PlayerState.CurrentStep += SelectNum;

        if (col.gameObject.name == "Duel")
        {
            TempAudio.volume = SoundState.musicVolume;
            TempAudio.PlayOneShot(Clips[4]);
            Dice.GetComponent<DiceMove>().Selected = false;
            DiceSelected = false;
            FadeOut.SetActive(true);
            check = true;
            NextScene = "Duel1";
        }
        else if (col.gameObject.name == "TempStore")
        {
            TempAudio.volume = SoundState.musicVolume;
            TempAudio.PlayOneShot(Clips[4]);
            Dice.GetComponent<DiceMove>().Selected = false;
            DiceSelected = false;
            FadeOut.SetActive(true);
            check = true;
            NextScene = "TempStore";
        }
        else if (col.gameObject.name == "Damaged")
        {
            TempAudio.volume = SoundState.musicVolume;
            TempAudio.PlayOneShot(Clips[3]);
            PlayerState.HP = (PlayerState.HP - 5 > 0 ? PlayerState.HP - 5 : 0);
            m_anim.SetTrigger("Hurt");
            if(PlayerState.HP <= 0f)
                Loading.loading("FailEnding");

            StartCoroutine("WaitAndSetSelect");
        }
        else if (col.gameObject.name == "LossCoin")
        {
            TempAudio.volume = SoundState.musicVolume;
            TempAudio.PlayOneShot(Clips[2]);
            PlayerState.Coin = (PlayerState.Coin - 5 > 0 ? PlayerState.Coin - 5 : 0);
            GameObject.Find("MainUI").GetComponent<MainUI>().ChangeCoinUI();
            m_anim.SetTrigger("Hurt");
            StartCoroutine("WaitAndSetSelect");
        }
        else if (col.gameObject.name == "Heal")
        {
            TempAudio.volume = SoundState.musicVolume;
            TempAudio.PlayOneShot(Clips[1]);
            PlayerState.HP = (PlayerState.HP + 5 < PlayerState.MaxHP ? PlayerState.HP + 5 : PlayerState.MaxHP);
            m_anim.SetTrigger("Victory");
            StartCoroutine("WaitAndSetSelect");
        }
        else if (col.gameObject.name == "GetCoin")
        {
            TempAudio.volume = SoundState.musicVolume;
            TempAudio.PlayOneShot(Clips[0]);
            PlayerState.Coin += 5;
            GameObject.Find("MainUI").GetComponent<MainUI>().ChangeCoinUI();
            m_anim.SetTrigger("Victory");
            StartCoroutine("WaitAndSetSelect");
        }
    }

    IEnumerator WaitAndFinshDongeon()
    {
        yield return new WaitForSeconds(1.5f);
        check = true;
        FadeOut.SetActive(true);

        if (PlayerState.CurrentQuest == PlayerState.TotalQuest[PlayerState.goal])
            NextScene = "SuccessEnding";
        else
            NextScene = "Village";
    }

    IEnumerator WaitAndSetSelect()
    {
        yield return new WaitForSeconds(1.5f);
        Dice.GetComponent<DiceMove>().Selected = false;
        DiceSelected = false;
        Dice.SetActive(true);
        Dice.transform.position = GameObject.Find("DicePosition").transform.position;
    }
}