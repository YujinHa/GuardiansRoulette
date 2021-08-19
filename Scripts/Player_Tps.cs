using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player_Tps : MonoBehaviour
{
    Rigidbody m_rgbd;
    Animator m_anim;

    private Transform _transform;
    private float _horizontal = 0.0f;
    private float _vertical = 0.0f;

    public float moveSpd = 5.0f;
    public float rotateSpd = 300.0f;
    private bool m_Jump;
    public float m_JumpPower = 4f;
    bool m_IsGrounded;
    private CapsuleCollider m_capsuleCollider;
    bool m_IsOnDongeonPortal = false;

    private PlayerState m_PlayerState;
    private VillageCameraManager m_VillageCameraManager;
    public GameObject VillageCameraManager;
    private Store m_Store;
    public GameObject Store;

    public GameObject FadeOut;
    float finishtime = 0f;
    public bool check = false;
    public string NextScene;

    public AudioSource TempAudio;
    public AudioClip Clip;

    void Start()
    {
        GameObject.Find("SoundState").GetComponent<AudioSource>().clip = GameObject.Find("SoundState").GetComponent<SoundState>().audioClip[10];
        GameObject.Find("SoundState").GetComponent<AudioSource>().Play();
        FadeOut.SetActive(false);
        PlayerState.mapStates = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        m_rgbd = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
        m_capsuleCollider = GetComponent<CapsuleCollider>();
        m_rgbd.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        m_VillageCameraManager = VillageCameraManager.GetComponent<VillageCameraManager>();
        m_Store = Store.GetComponent<Store>();
        m_PlayerState = GameObject.Find("PlayerState").GetComponent<PlayerState>();

        transform.position = PlayerState.VillagePlayerPosition;
        transform.eulerAngles = PlayerState.VillagePlayerRotation;
    }

    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        //상점 안 들어가있을 때만 입력 이동
        if(m_VillageCameraManager.MainCam.activeSelf && (!m_IsOnDongeonPortal))
        {
            IsGround();
            if (!m_Jump)
            {
                if (Input.GetButtonDown("Jump") && m_IsGrounded)
                {
                    m_Jump = true;
                    Jump();
                }
            }
            Move();
            m_Jump = false;
        }

        if (check == true)
        {
            FadeOut.SetActive(true);
            finishtime = finishtime + Time.deltaTime;
        }

        if (finishtime >= 1.0f)
            Loading.loading(NextScene);

        if (Input.GetKeyDown(KeyCode.F12))
        {
            CheatKey.SetCheatKey();
        }
    }

    private void Jump()
    {
        m_rgbd.velocity = transform.up * m_JumpPower;
    }

    public void Move()
    {
        Vector3 moveDirect = (Vector3.forward * _vertical) + (Vector3.right * _horizontal);
        _transform.Translate(moveDirect.normalized * Time.deltaTime * moveSpd, Space.Self);
        _transform.Rotate(Vector3.up * Time.deltaTime * rotateSpd * Input.GetAxis("Mouse X"));

        UpdateAnimator();
    }

    private void IsGround()
    {
        m_IsGrounded = Physics.Raycast(transform.position + (Vector3.up * 0.5f), Vector3.down, m_capsuleCollider.bounds.extents.y + 0.1f);
    }

    void UpdateAnimator()
    {
        m_anim.SetBool("OnGround", m_IsGrounded);//
        if ((_horizontal != 0.0f) || (_vertical != 0.0f))
            m_anim.SetBool("Walk", true);
        else
            m_anim.SetBool("Walk", false);

        if (m_Jump)
            m_anim.SetBool("Jump", true);
        else
            m_anim.SetBool("Jump", false);
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "StoreCollider")
        {
            TempAudio.volume = SoundState.musicVolume;
            TempAudio.PlayOneShot(Clip);
            m_VillageCameraManager.SetStoreCam();
            m_Store.EnterStore();
        }
        if (col.tag == "HospitalCollider")
        {
            TempAudio.volume = SoundState.musicVolume;
            TempAudio.PlayOneShot(Clip);
            check = true;
            FadeOut.SetActive(true);
            NextScene = "Hospital";
        }
        if (col.tag == "PetHouseCollider")
        {
            TempAudio.volume = SoundState.musicVolume;
            TempAudio.PlayOneShot(Clip);
            check = true;
            FadeOut.SetActive(true);
            NextScene = "PetHouse";
        }
        if (col.tag == "TrainingCenterCollider")
        {
            TempAudio.volume = SoundState.musicVolume;
            TempAudio.PlayOneShot(Clip);
            check = true;
            FadeOut.SetActive(true);
            NextScene = "TrainingCenter";
        }
        if (col.tag == "DongeonPortal")
        {
            TempAudio.volume = SoundState.musicVolume;
            TempAudio.PlayOneShot(Clip);
            m_IsOnDongeonPortal = true;
            PlayerState.CurrentQuest++;
            SceneChangeToDongeon();
        }
    }

    void SceneChangeToDongeon()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(Clip);
        check = true;
        FadeOut.SetActive(true);
        NextScene = "Dongeon";
    }
}