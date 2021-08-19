using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTime : MonoBehaviour
{
    public Text CoolTimeText;
    private int CoolTimer;

    void Start()
    {
        
    }
    
    void Update()
    {
        CoolTimer = (int)PlayerState.coolTime_Hospital;
        CoolTimeText.text = CoolTimer.ToString() + "초";
    }
}
