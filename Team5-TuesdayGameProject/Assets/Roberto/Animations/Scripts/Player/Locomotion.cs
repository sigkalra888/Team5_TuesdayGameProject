using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion : MonoBehaviour
{
    [HideInInspector]
    public CharacterController controller;
    public float vertical;
    public float horizontal;
    [HideInInspector]
    public Animator anim;
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
    public float currentSpeed;
    float velocity_Y=0;
    [HideInInspector]
    public ClimbingControll climb_controll;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        model = anim.gameObject;
        controller = GetComponent<CharacterController>();
        hands = GetComponent<PlayerHands>();
        climb_controll = new ClimbingControll(this);
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
        bool interacting = hands.interacting;
        if (interacting) return;

        if (climb_controll.climbing)
        {
            climb_controll.climbHandler();
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
           if (Input.GetKey(KeyCode.M))
            {
               
                Collider[] colls = Physics.OverlapBox(transform.position,new Vector3(2,2,2));
                foreach(var c in colls)
                {
                    if(c.gameObject.tag=="Climb")
                    {
                        climb_controll.climbing = true;
                        anim.SetBool("climbing", climb_controll.climbing);
                        anim.SetTrigger("climb");
                        
                    }
                }
            }
        }
        
    }
    void animationHandler()
    {
        //pushing animation
        anim.SetBool("push", pushing);
        //moving
        anim.SetFloat("Vertical",currentSpeed, speedSmoothTime, Time.deltaTime);
    }

 
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,new Vector3(2,2,2));
    }
}
