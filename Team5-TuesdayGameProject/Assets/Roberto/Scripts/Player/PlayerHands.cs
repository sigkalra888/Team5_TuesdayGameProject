using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    [SerializeField]
    float radius = 5;
    //
    [SerializeField]
    int cold = -10;
    [SerializeField]
    int hot = 10;
    [SerializeField]
    KeyCode key = KeyCode.F;
    [SerializeField]
    KeyCode up_Hot = KeyCode.UpArrow;
    [SerializeField]
    KeyCode down_Cold = KeyCode.DownArrow;
    void Update()
    {
        //状態の変更
        bool interacting = Input.GetKey(key);
        
        if(interacting)
        {
            if(Input.GetKey(up_Hot))
            {
                //温度の変更
                UseElement(Element.hot);
                //温度+値
                changeGradeToTarget(hot);
            }

            if (Input.GetKey(down_Cold))
            {
                //温度の変更
                UseElement(Element.cold);
                //温度+ (-値）
                changeGradeToTarget(cold);
            }

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
