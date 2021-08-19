using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public GameObject inform1;
    public GameObject inform2;
    public GameObject informNextButton;
    public GameObject informBackButton;

    void Start()
    {
        inform1.SetActive(false);
        inform2.SetActive(false);
    }

    void Update()
    {

    }

    public void StartClick()
    {
        SceneManager.LoadScene("Village");
    }

    public void InformClick()
    {
        inform1.SetActive(true);
        informNextButton.SetActive(true);
    }

    public void InformNextClick()
    {
        inform1.SetActive(false);
        inform2.SetActive(true);
        informNextButton.SetActive(false);
        informBackButton.SetActive(true);
    }

    public void ExitClick()
    {
        Application.Quit();
    }
}
