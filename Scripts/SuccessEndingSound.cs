using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessEndingSound : MonoBehaviour
{
    void Start()
    {

//        GameObject.Find("SoundState").GetComponent<AudioSource>().clip = GameObject.Find("SoundState").GetComponent<SoundState>().audioClip[20];
        GameObject.Find("SoundState").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("SoundState").GetComponent<SoundState>().audioClip[20]);
    }
    
    void Update()
    {
        
    }
}
