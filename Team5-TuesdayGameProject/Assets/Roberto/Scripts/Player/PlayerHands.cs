using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    public float radius = 5;
    //
    public  int cold = -10;
    public int hot = 10;

    void Update()
    {
        //状態の変更
        if(Input.GetKeyDown(KeyCode.Z))
        {
            UseElement(Element.hot);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            UseElement(Element.cold);
        }

        //温度の変更
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //温度+値
            changeGradeToTarget(hot);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //温度+ (-値）
            changeGradeToTarget(cold);
        }

    }
    //for debug
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    // //状態の変更
    void UseElement(Element e)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        //use sphere raycast for manipulate adiacent objects
        foreach(var c in colliders)
        {
            Imanipulate manipulate = c.GetComponent<Imanipulate>();
            if(manipulate!=null)
            {
                manipulate.Change(e);
            }
        }
    }
    // //温度の変更
    void changeGradeToTarget(int value)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        //use sphere raycast for manipulate adiacent objects
        foreach (var c in colliders)
        {
            ICelsius manipulate = c.GetComponent<ICelsius>();
            if (manipulate != null)
            {
                manipulate.Change(value);
            }
        }
    }
}
