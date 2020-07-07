using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    [SerializeField]
    float radius = 5;
   
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
    
    //箱の移動のため
    [SerializeField]
    KeyCode push_box_Key = KeyCode.P;

    GameObject box;
    public bool HASBOX() { return hasBox; }
    bool hasBox = false;
    public bool pushBox;
    void Update()
    {
        //状態の変更
        bool interacting = Input.GetKey(key);
        pushBox = Input.GetKey(push_box_Key);
        //箱
        if (pushBox && !hasBox)
        {
            TakingBox();

        }
        //温度
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
        //箱をやめる
        if(pushBox==false)
        {
            if(hasBox)
            {
                releaseBox();
            }
            
        }
    }
    //for debug
 /*   private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }*/

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

    #region BOX
    void releaseBox()
    {
        hasBox = false;
        box.transform.SetParent(null);
        box = null;
    }

    void TakingBox()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius/2);
        //use sphere raycast for manipulate adiacent objects
        foreach (var c in colliders)
        {
            if (c.gameObject.tag=="Box")
            {
                hasBox = true;
                box = c.gameObject;
                box.transform.SetParent(this.transform);
                box.transform.localPosition = new Vector3(0,0,1);
                break;
            }
        }
    }
    #endregion
}
