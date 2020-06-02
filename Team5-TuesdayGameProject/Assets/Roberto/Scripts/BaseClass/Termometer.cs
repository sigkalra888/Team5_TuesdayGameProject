using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Termometer : MonoBehaviour, ICelsius
{
    Color red = Color.red;
    Color blue = Color.blue;
    Color white = Color.white;

    MeshRenderer rend;
    public int initialGrades = 20;
    [SerializeField]
    int grades = 20;
    public float resetTime = 2;
    float couter = 0;
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
        statusCheck();
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
    public void Change(int grade)
    {
        grades += grade;
    }
    void statusCheck()
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

