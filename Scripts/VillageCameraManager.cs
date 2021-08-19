using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageCameraManager : MonoBehaviour
{
    public GameObject MainCam;
    public GameObject StoreCam;

    void Start()
    {
        MainCam.SetActive(true);
        StoreCam.SetActive(false);
    }
    
    void Update()
    {
    }

    public void SetStoreCam()
    {
        StoreCam.SetActive(true);
        MainCam.SetActive(false);
    }

    public void SetMainCam()
    {
        StoreCam.SetActive(false);
        MainCam.SetActive(true);
    }
}
