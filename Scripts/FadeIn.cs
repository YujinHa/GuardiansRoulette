using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    float finishtime = 0f;

    void Start()
    {
        
    }
    
    void Update()
    {
        finishtime = finishtime + Time.deltaTime;

        if (finishtime >= 1f)
        {
            gameObject.SetActive(false);
        }
    }
}
