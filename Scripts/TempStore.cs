using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TempStore : MonoBehaviour
{
    public GameObject m_ItemSet;
    public GameObject m_PetItemSet;
    public GameObject BuyButton;

    public Text ItemSelectButtonText;
    public Text PetItemSelectButtonText;
    public Text ItemInformtText;
    
    public int currentSelectItem;

    public GameObject FadeOut;
    float finishtime = 0f;
    bool check = false;
    private string NextScene;
    public AudioSource TempAudio;
    public AudioClip[] Clips;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject.Find("SoundState").GetComponent<AudioSource>().clip = GameObject.Find("SoundState").GetComponent<SoundState>().audioClip[7];
        GameObject.Find("SoundState").GetComponent<AudioSource>().Play();
        FadeOut.SetActive(false);
        PlayerState.mapStates = 3;
        m_PetItemSet.SetActive(false);
        m_ItemSet.SetActive(true);
        ItemSelectButtonText.color = Color.yellow;
        PetItemSelectButtonText.color = Color.white;
        BuyButton.SetActive(false);
    }

    void Update()
    {
        if (check == true)
            finishtime = finishtime + Time.deltaTime;

        if (finishtime >= 1.0f)
            Loading.loading(NextScene);

    }

    public void BackClick_Store()
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(Clips[0]);
        check = true;
        FadeOut.SetActive(true);
        NextScene = "Dongeon";
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
