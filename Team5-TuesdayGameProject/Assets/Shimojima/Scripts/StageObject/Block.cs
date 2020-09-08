using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour
{
    //温度交換用
    public string temperature = "";

    //初期温度
    [SerializeField]
    private string defaultTemperature = "";

    //温度交換されたか否か
    public bool changeTemperature = false;

    //温度交換されてから初期温度に戻るまでの時間
    [SerializeField]
    private float resetTime = 0;
    private float totalTime = 0;

    [HideInInspector]
    public bool isSelected = false; //交換の対象になっているかの判定
    [HideInInspector]
    public bool isTarget = false;   //プレイヤーに選択されているかの判定

    public GameObject highLightObj;

    [SerializeField]
    private float c_ChageSpeed1 = 0;
    [SerializeField]
    private float c_ChageSpeed2 = 0;
    [SerializeField]
    private float c_ChageSpeed3 = 0;

    private Color myC;
    private Color hot = new Color(1, 0.391f, 0.391f, 1);
    private Color ice = new Color(0.391f, 0.391f, 1, 1);
    private Color normal = new Color(0.51f, 0.51f, 0.51f, 1);

    private string brockName = "";
    private float time;

    private void Awake()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "StageEdit") { return; }
        //初期温度に変更
        temperature = defaultTemperature;
        if (temperature != "Hot")
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        brockName = name;
        myC = GetComponent<MeshRenderer>().material.color;

    }

    void Update()
    {
        if (name == "HotBlock")
        {
            if (temperature == "Hot")
            {
                time += (Time.deltaTime / 40);

                if (time >= 1)
                {
                    float t = time - 1;
                    time = t;
                }

                GetComponent<MeshRenderer>().material.SetFloat("_DeltaTime", time);
            }
            
        }

        ReturnTemperature();

        if (!isTarget && !isSelected && highLightObj.activeSelf) { highLightObj.SetActive(false); }
        else if(isTarget && !highLightObj.activeSelf) { highLightObj.SetActive(true); }
    }

    //温度交換による色の変化
    public void BlockColorChange()
    {
        switch (temperature)
        {
            case "Hot":
                GetComponent<MeshRenderer>().material.color = hot;
                transform.GetChild(1).gameObject.SetActive(true);
                myC = hot;
                break;
            case "Ice":
                GetComponent<MeshRenderer>().material.color = ice;
                transform.GetChild(1).gameObject.SetActive(false);
                myC = ice;
                break;
            case "Normal":
                GetComponent<MeshRenderer>().material.color = normal;
                transform.GetChild(1).gameObject.SetActive(false);
                myC = normal;
                break;
        }
        totalTime = 0;
    }

    /// <summary>
    /// 初期温度に戻る処理
    /// </summary>
    private void ReturnTemperature()
    {
        if (!changeTemperature) { return; }
        if(temperature == defaultTemperature) { changeTemperature = false;}
        totalTime += Time.deltaTime;

        if (totalTime > resetTime - 1)
        {
            BlockColorReturn();
        }

        //元の温度に変更
        if (totalTime > resetTime)
        {
            temperature = defaultTemperature;
            totalTime = 0;
            changeTemperature = false;
        }
    }

    private void BlockColorReturn()
    {
        switch (defaultTemperature)
        {
            case "Hot":
                transform.GetChild(1).gameObject.SetActive(true);
                DoChangeHotColor();
                break;
            case "Ice":
                transform.GetChild(1).gameObject.SetActive(false);
                DoChangeIceColor();
                break;
            case "Normal":
                transform.GetChild(1).gameObject.SetActive(false);
                DoChangeNormalColor();
                break;
        }
    }

    private void DoChangeHotColor()
    {
        if (temperature == "Ice")
        {
            GetComponent<MeshRenderer>().material.color = myC;
            myC = new Color(myC.r + c_ChageSpeed1, myC.g, myC.b - c_ChageSpeed1, myC.a);
        }
        else if(temperature == "Normal")
        {
            GetComponent<MeshRenderer>().material.color = myC;
            myC = new Color(myC.r + c_ChageSpeed2, myC.g - c_ChageSpeed3, myC.b - c_ChageSpeed3, myC.a);
        }
    }

    private void DoChangeIceColor()
    {
        if (temperature == "Hot")
        {
            GetComponent<MeshRenderer>().material.color = myC;
            myC = new Color(myC.r - c_ChageSpeed1, myC.g, myC.b + c_ChageSpeed1, myC.a);
        }
        else if (temperature == "Normal")
        {
            GetComponent<MeshRenderer>().material.color = myC;
            myC = new Color(myC.r - c_ChageSpeed3, myC.g - c_ChageSpeed3, myC.b + c_ChageSpeed2, myC.a);
        }
    }

    private void DoChangeNormalColor()
    {
        if (temperature == "Hot")
        {
            GetComponent<MeshRenderer>().material.color = myC;
            myC = new Color(myC.r - c_ChageSpeed2, myC.g + c_ChageSpeed3, myC.b + c_ChageSpeed3, myC.a);
        }
        else if (temperature == "Ice")
        {
            GetComponent<MeshRenderer>().material.color = myC;
            myC = new Color(myC.r + c_ChageSpeed3, myC.g + c_ChageSpeed3, myC.b - c_ChageSpeed2, myC.a);
        }
    }
}
