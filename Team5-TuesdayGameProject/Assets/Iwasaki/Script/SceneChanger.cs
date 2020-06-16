using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : SingletonMonoBehaviour<SceneChanger>
{
    [HideInInspector]
    public bool toStageSelectBool = false;   
    [HideInInspector]
    public bool toTitleBool = false;

    //Stageの種類によって増減あり。
    [HideInInspector]
    public bool toStage1Bool = false;
    [HideInInspector]
    public bool toStage2Bool = false;

    private GameObject fadeCanvas;

    private void Start()
    {
        //フェード用のキャンバス作成
        fadeCanvas = new GameObject("FadeCanvas");
        fadeCanvas.transform.SetParent(transform);

        Canvas canvas = fadeCanvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;
        fadeCanvas.AddComponent<CanvasGroup>();
        fadeCanvas.GetComponent<CanvasGroup>().alpha = 0;

        //フェード用の画像作成
        GameObject imageObject = new GameObject("Image");
        imageObject.transform.SetParent(fadeCanvas.transform, false);
        imageObject.AddComponent<Image>().color = Color.black;
        imageObject.GetComponent<RectTransform>().sizeDelta = new Vector2(2000, 2000);
    }
    void Update()
    {
        if (toStageSelectBool)
        {
            toStageSelectBool = false;
            StartCoroutine(FadeOut(1.5f));
            SceneManager.LoadScene("StageSelect");
        }

        if (toTitleBool)
        {
            toTitleBool = false;
            SceneManager.LoadScene("Title");
        }
        StageSelect();
    }
    private void StageSelect()
    {
        if (toStage1Bool)
        {
            toStage1Bool = false;
            StartCoroutine(FadeOut(1.5f));    
        }

        if (toStage2Bool)
        {
            toStage2Bool = false;
            SceneManager.LoadScene("Stage2");
        }
    }

    IEnumerator FadeOut(float fadeTime)
    {
        Debug.Log("inFade");
        float time = 0f;
        while (fadeCanvas.GetComponent<CanvasGroup>().alpha < 1)
        {
            fadeCanvas.GetComponent<CanvasGroup>().alpha = 1f * (time / fadeTime);
            time += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene("Stage1");
        yield break;
    }
}
