using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion : MonoBehaviour
{
    CharacterController controller;
    float vertical;
    float horizontal;
    Animator anim;
    GameObject model;
    PlayerHands hands;
    bool pushing = false;
    [SerializeField]
    float moveSpeed = 5;
    [SerializeField]
    float jumpForce = 25;
    [SerializeField]
    float gravityForce = 12;
    /// 
    float turnSmoothTime=0.2f;
    float turnSmoothVelocity;
    bool jumping = false;
    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    float velocity_Y=0;
    //climb
    bool climbing = false;
    bool climbingUp = false;
    [HideInInspector]
    public Vector3 top;
    bool reachTheTop = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        model = anim.gameObject;
        controller = GetComponent<CharacterController>();
        hands = GetComponent<PlayerHands>();
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        pushing = hands.pushBox;
    }
    // Update is called once per frame
    void Update()
    {
        if (climbing)
        {
            climbHandler();
           
        }
        else
        {
            Move();
        }
       
        animationHandler();
    }

    private void Jump()
    {
        if(controller.isGrounded)
        {
            float jumpvelocity = Mathf.Sqrt(2 * gravityForce * jumpForce);
            velocity_Y = jumpvelocity;
            anim.SetTrigger("jump");
        }
    }
void Move()
    {
        Vector2 input = new Vector2(horizontal, vertical);
        Vector2 inputDir = input.normalized;
        //rotation
        if (inputDir != Vector2.zero)
        {
            float targetRot = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRot, ref turnSmoothVelocity, turnSmoothTime);
        }
        float targetSpeed = moveSpeed * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, turnSmoothTime);
        //applay gravity force
        velocity_Y -= Time.deltaTime * gravityForce;
        //movement
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocity_Y;
        controller.Move(velocity * Time.deltaTime);
        //interpolate current speed
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;
        //ground check, reset value
        if (controller.isGrounded)
        {
            velocity_Y = 0;
        }
        //jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        jumping = !controller.isGrounded;
        if(jumping)
        {
            reachTheTop = false;
            if (Input.GetKey(KeyCode.M))
            {
               
                Collider[] colls = Physics.OverlapBox(transform.position,new Vector3(2,2,2));
                foreach(var c in colls)
                {
                    if(c.gameObject.tag=="Climb")
                    {
                        climbing = true;
                        anim.SetBool("climbing", climbing);
                        anim.SetTrigger("climb");
                        
                    }
                }
            }
        }
        climbingUp = false;
    }
    void animationHandler()
    {
        //pushing animation
        anim.SetBool("push", pushing);
        //moving
        anim.SetFloat("Vertical",currentSpeed, speedSmoothTime, Time.deltaTime);
    }

    void climbHandler()
    {
        if(!reachTheTop)
        {
            transform.localScale = new Vector3(1, 1, 1);
            Vector3 velocity = transform.up * vertical;
            currentSpeed = velocity.magnitude;
            //pushing animation
            controller.Move(velocity * Time.deltaTime);
        }
        else
        {
            currentSpeed = 0;
        }
        //jump down
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dislocate();

        }
        FindTop();
        if(reachTheTop&&Input.GetKeyDown(KeyCode.N))
        {
            StartCoroutine(goUpCO());
        }
    }


    IEnumerator goUpCO()
    {
        anim.SetTrigger("ClimbUP");
        yield return new WaitForSeconds(3f);
        climbing = false;
       // anim.SetBool("climbing",climbing);
        yield return null;
        
    }

    void FindTop()
    {
        
        //check
        Collider[] colls = Physics.OverlapBox(transform.position, new Vector3(2,2, 2));
        foreach (var c in colls)
        {
            if (c.gameObject.tag == "Top")
            {
                reachTheTop = true;
                //get position of child element
                top = c.transform.GetChild(0).transform.position;
            }
        }
        
    }
    public void setTheTopPos()
    {
        transform.position = top;
    }
    //go downfrom wall
    void dislocate()
    {
        reachTheTop = false;
        climbing = false;
        anim.SetBool("climb", climbing);
        transform.localScale = new Vector3(1, 1, 1);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,new Vector3(2,2,2));
    }
}
