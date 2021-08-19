using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoManager : MonoBehaviour
{
    float finishtime = 0f;
    public GameObject title;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public GameObject FadeOut;

    private void Awake()
    {
        Screen.SetResolution(1920, 1080, false);
    }

    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        FadeOut.SetActive(false);
    }

    void Update()
    {
        finishtime = finishtime + Time.deltaTime;
        
        if (finishtime >= 2.0f)
        {
            FadeOut.SetActive(true);
        }
        if (finishtime >= 3.0f)
        {
            Loading.loading("Story");
        }

        if (title.transform.localScale.x <= 1.0)
            title.transform.localScale += new Vector3(0.01f, 0.01f, 0f);
    }
}
