using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailEndingSound : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("SoundState").GetComponent<AudioSource>().clip = GameObject.Find("SoundState").GetComponent<SoundState>().audioClip[19];
        GameObject.Find("SoundState").GetComponent<AudioSource>().Play();
    }
    
    void Update()
    {
        
    }
}
