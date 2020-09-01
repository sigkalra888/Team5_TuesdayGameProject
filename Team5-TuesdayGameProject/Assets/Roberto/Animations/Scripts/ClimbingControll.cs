using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClimbingControll 
{
    Vector3 destination=Vector3.zero;
    public ClimbingControll(Locomotion p)
    {
        player = p;
        transform = player.transform;
       
    }
    //climb
    public bool climbing = false;
    public bool climbingUp = false;
    [HideInInspector]
    public Vector3 top;
    public bool reachTheTop()
    {
        //check
        Collider[] colls = Physics.OverlapBox(transform.position, new Vector3(2f,1f,2f));
        foreach (var c in colls)
        {
            if (c.gameObject.tag == "Top")
            {
               //get position of child element
                top = c.transform.GetChild(0).transform.position;
                return true;
            }
        }
        return false;
    }
    Locomotion player;
    Transform transform;
    bool moving = false;
    float security_timer = 2;
    public void climbHandler()
    {
        bool Top = reachTheTop();
        if (Top)
        {
            player.currentSpeed = 0;
            if (Input.GetKeyDown(KeyCode.N))
            {
                MoveToTheTop();
            }
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            Vector3 velocity = transform.up * player.vertical;
            player.currentSpeed = velocity.magnitude;
            //pushing animation
            player.controller.Move(velocity * Time.deltaTime);
        }
        //jump down
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dislocate();
            player.anim.SetTrigger("jump");
        }
        if(moving)
        {
            MoveTO();
            if(transform.position==destination)
            {
                dislocate();
            }
            security_timer -= Time.deltaTime;
            //for stop processing
            if(security_timer<=0)
            {
                dislocate();
            }
        }
       
    }

    //go downfrom wall
    void dislocate()
    {
        moving = false;
        security_timer = 2;
        climbing = false;
        climbingUp = false;
        player.anim.SetBool("climb", false);
        transform.localScale = new Vector3(1, 1, 1);
        
    }

    public void MoveToTheTop()
    {
        //float distance=Vector3.dei
        player.anim.Play("ClimbUP");
        moving = true;
    }
     void MoveTO()
    {
        destination = new Vector3(top.x,top.y+1.5f,top.z);
        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime *0.5f); 
    }
    public void setTheTopPos()
    {
        transform.position = top;
    }
}
