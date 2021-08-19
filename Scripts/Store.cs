using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public GameObject m_BackButton;
    public GameObject m_ItemSet;
    public GameObject m_PetItemSet;
    public GameObject BuyButton;

    private VillageCameraManager m_VillageCameraManager;
    public GameObject VillageCameraManager;
    public GameObject Player;
    public GameObject StoreUI;
   
    public Text ItemSelectButtonText;
    public Text PetItemSelectButtonText;
    public Text ItemInformtText;

    public int currentSelectItem;
    public AudioSource TempAudio;
    public AudioClip[] Clips;

    void Start()
    {
        m_VillageCameraManager = VillageCameraManager.GetComponent<VillageCameraManager>();
        m_BackButton.SetActive(false);
        StoreUI.SetActive(false);
    }

    void Update()
    {

    }

    public void EnterStore()
    {
        PlayerState.mapStates = 3;
        m_BackButton.SetActive(true);
        StoreUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        m_PetItemSet.SetActive(false);
        m_ItemSet.SetActive(true);
        ItemSelectButtonText.color = Color.yellow;
        PetItemSelectButtonText.color = Color.white;
        BuyButton.SetActive(false);
    }

    public void BackClick_Store()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(Clips[0]);
        PlayerState.mapStates = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        m_BackButton.SetActive(false);
        StoreUI.SetActive(false);
        Player.transform.position = new Vector3(-0.45f, -0.2f, -32f);
        Player.transform.eulerAngles = new Vector3(0f, -90f, 0f);
        m_VillageCameraManager.SetMainCam();
    }

    public void Click_ItemSelectButton()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(Clips[1]);
        ItemSelectButtonText.color = Color.yellow;
        PetItemSelectButtonText.color = Color.white;
        m_PetItemSet.SetActive(false);
        m_ItemSet.SetActive(true);
        ItemInformtText.text = "";
        BuyButton.SetActive(false);
    }

    public void Click_PetItemSelectButton()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(Clips[1]);
        ItemSelectButtonText.color = Color.white;
        PetItemSelectButtonText.color = Color.yellow;
        m_ItemSet.SetActive(false);
        m_PetItemSet.SetActive(true);
        ItemInformtText.text = "";
        BuyButton.SetActive(false);
    }

    public void Click_Item(int num)
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(Clips[1]);
        ItemInformtText.text = ItemState.ItemInforms[num];
        currentSelectItem = num;

        if (PlayerState.Coin - ItemState.ItemPrice[currentSelectItem] >= 0)
            BuyButton.SetActive(true);
        else
            BuyButton.SetActive(false);
    }

    public void Click_BuyButton()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(Clips[2]);
        PlayerState.Coin -= ItemState.ItemPrice[currentSelectItem];
        ItemState.ItemCount[currentSelectItem]++;
        Click_Item(currentSelectItem);
    }
}
