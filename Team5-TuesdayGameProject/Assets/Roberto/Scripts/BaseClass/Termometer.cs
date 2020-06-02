using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Termometer : MonoBehaviour, ICelsius
{
    //debugの為
    Color red = Color.red;
    Color blue = Color.blue;
    Color white = Color.white;
　　MeshRenderer rend;

    //始まりの温度
    public int initialGrades = 20;
    //現在温度
    [SerializeField]
    int grades = 20;
    //リセット時間
    public float resetTime = 2;
    float couter = 0;
    //沸騰/
    bool isFrozen() { return grades <= 0; }
    bool Boil() { return grades >= 90; }
    

    private void Start()
    {
        grades = initialGrades;
        rend = GetComponent<MeshRenderer>();
        statusCheck();
    }
    private void Update()
    {
        //状況のアップデート
        statusCheck();
        //始めの温度と違うの時タイマーに沿って上がる又は減る
        bool reset = grades != initialGrades;
        if (!reset)
        {
            rend.material.color = white;
            return;
        }
        couter += Time.deltaTime;
        if(couter>=resetTime)
        {
            couter = 0;
            if (grades > initialGrades)
            {
                grades--;
            }
            else if(grades<initialGrades)
            {
                grades++;
            }
            
        }
       
    }
    //温度を増える/減る関数
    public void Change(int grade)
    {
        grades += grade;
    }
    
    //暖かいや冷たいの時の処理
    public virtual void statusCheck()
    {
        if (Boil())
        {
            boiling(); 
        }
        if (isFrozen())
        {
            frozing();
        }
    }
    public virtual void boiling()
    {
        rend.material.color = red;
    }
    public virtual void frozing()
    {
        rend.material.color = blue;
    }
}

