using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlide : MonoBehaviour
{

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void SetVolume()
    {
        SoundState.musicVolume = GameObject.Find("Slider").GetComponent<Slider>().value;
    }
}
