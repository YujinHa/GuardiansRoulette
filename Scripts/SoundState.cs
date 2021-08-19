using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundState : MonoBehaviour
{
    private static SoundState S_Instance = null;

    public static AudioSource audioSrc;
    public AudioClip[] audioClip;
//    public static AudioClip[] audioClip;
    static public float musicVolume = 1f;

    private void Awake()
    {
        if (S_Instance)
        {
            DestroyImmediate(this.gameObject);
            return;
        }

        S_Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        audioSrc.volume = musicVolume;
    }
}
