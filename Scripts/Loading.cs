using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField]
    Image loadingBar;
    public static string sceneName;

    void Start()
    {
        loadingBar.fillAmount = 0;
        StartCoroutine(LoadSceneAsy());
    }
    
    void Update()
    {

    }

    public static void loading(string sceneNametemp)
    {
        SceneManager.LoadScene("Loading");
        sceneName = sceneNametemp;
    }

    IEnumerator LoadSceneAsy()
    {
        yield return null;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        float timeTemp = 0;
        
        while (!asyncLoad.isDone)
        {
            yield return null;
            timeTemp += Time.deltaTime;

            if (asyncLoad.progress >= 0.9f)
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1, timeTemp);
                if (loadingBar.fillAmount == 1.0f)
                    asyncLoad.allowSceneActivation = true;
            }
            else
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, asyncLoad.progress, timeTemp);
                if (loadingBar.fillAmount >= asyncLoad.progress)
                    timeTemp = 0f;
            }
        }
    }
}
