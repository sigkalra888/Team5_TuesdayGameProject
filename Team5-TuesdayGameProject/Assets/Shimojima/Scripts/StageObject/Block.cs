using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //温度交換用
    public float temperature = 0;

    //初期温度
    [SerializeField]
    private float defaultTemperature = 0;

    //温度交換されたか否か
    [SerializeField]
    private bool changeTemperature = false;

    //温度交換されてから初期温度に戻るまでの時間
    [SerializeField]
    private float resetTime = 0;
    private float totalTime = 0;

    private Color hot = new Color(1, 0.59f, 0.59f, 1);
    private Color ice = new Color(0.59f, 0.59f, 1, 1);

    private void Awake()
    {
        //初期温度に変更
        temperature = defaultTemperature;
        GetComponent<MeshRenderer>().material.color = hot;
    }

    void Update()
    {
        ReturnTemperature();
    }

    /// <summary>
    /// 初期温度に戻る処理
    /// </summary>
    private void ReturnTemperature()
    {
        if (!changeTemperature) { return; }
        totalTime += Time.deltaTime;
        if (totalTime > resetTime)
        {
            temperature = defaultTemperature;
            totalTime = 0;
            changeTemperature = false;
        }
    }
}
