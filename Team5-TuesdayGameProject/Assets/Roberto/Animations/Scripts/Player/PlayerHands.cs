using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    [SerializeField]
    float radius = 5;

    [SerializeField]
    KeyCode InvertInput = KeyCode.F;
    //箱の移動のため
    [SerializeField]
    KeyCode push_box_Key = KeyCode.P;

    GameObject box;
    public bool HASBOX() { return hasBox; }
    bool hasBox = false;
    public bool pushBox;
    Animator anim;
    public bool interacting;
    int Navigation = 0;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        pushBox = Input.GetKey(push_box_Key);
        bool invert = Input.GetKey(InvertInput);
        //箱
        if (pushBox && !hasBox)
        {
            TakingBox();

        }
        //箱をやめる
        if(pushBox==false)
        {
            if(hasBox)
            {
                releaseBox();
            }
            
        }
        if(invert)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            GameObject block=null;
            //create list and apply manipulation
            foreach (var c in colliders)
            {
                Block manipulate = c.GetComponent<Block>();
                if (manipulate != null)
                {
                    block = manipulate.gameObject;
                    break;
                }
            }
            if (block == null) return;
            anim.Play("O_Magic");
            BlockManager.Instance.ChangeBlock(block);
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




/*[SerializeField]
int cold = -10;
[SerializeField]
int hot = 10;
[SerializeField]
KeyCode key = KeyCode.Z;
[SerializeField]
KeyCode up_Hot = KeyCode.UpArrow;
[SerializeField]
KeyCode down_Cold = KeyCode.DownArrow;*/


/* // //状態の変更
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
 }*/
/* //温度
 * 
        //状態の変更
        interacting = Input.GetKey(key);

    if(interacting)
        {
            if(Input.GetKeyDown(up_Hot))
            {
                anim.Play("TH_Magic");
                //温度の変更
                UseElement(Element.hot);
                //温度+値
                changeGradeToTarget(hot);
            }

            if (Input.GetKeyDown(down_Cold))
            {
                anim.Play("O_Magic");
                //温度の変更
                UseElement(Element.cold);
                //温度+ (-値）
                changeGradeToTarget(cold);
            }

        }*/
