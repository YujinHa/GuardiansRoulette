using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Highlight : MonoBehaviour
{
    public GameObject[] sides;
    public AudioSource TempAudio;
    public AudioClip DiceClip;

    void Start()
    {
        for (int i = 0; i < sides.Length; i++)
        {
            sides[i].SetActive(false);
        }
    }

    void Update()
    {
    }

    public void HighlightSides(int side)
    {
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(DiceClip);
        sides[side - 1].SetActive(true);
    }

    public void SetHighlight()
    {
        for (int i = 0; i < sides.Length; i++)
        {
            sides[i].SetActive(false);
        }
    }
}
