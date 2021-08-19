using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteParticle : MonoBehaviour
{
    private float removeTime = 2.0f;

    void Start()
    {
        Destroy(gameObject, removeTime);
    }
    
    void Update()
    {
        
    }
}
