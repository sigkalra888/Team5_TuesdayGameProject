using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform senter;
    [SerializeField]
    private float accelaration, decay = 0;
    [SerializeField]
    private float maxSpeed = 0;
    private float moveSpeed = 0;

    private float h, v = 0;
    [SerializeField]
    private float cameraAngle = 0;
    private float sumAngle = 0;

    private bool isRotate = false;
    private int rotateArrowAngle = 0;
    private bool isWalk = false;

    private Vector3 myPos = Vector3.zero;
    private Quaternion myQ = new Quaternion();

    private Animator animator;

    [SerializeField]
    private Image selectUI;
    [SerializeField]
    private Sprite[] images;

    private GameObject targetObj;

    void Start()
    {
        //senterオブジェクトがない場合は生成して格納
        if(senter == null)
        {
            senter = new GameObject().transform;
            senter.position = new Vector3(0, 20, 0);
        }
        if(accelaration == 0){ accelaration = 0; }
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        myPos = transform.position;
        myQ = transform.rotation;

        if (h  != 0 || v != 0)
        {
            if (!isWalk) { isWalk = true; animator.SetTrigger("WalkStart"); }

            //プレイヤーの加速
            if (moveSpeed < maxSpeed)
            {
                moveSpeed += accelaration;
            }
        }
        else
        {
            if (isWalk) { isWalk = false; animator.SetTrigger("WalkEnd"); }
            //プレイヤーの減速
            if (moveSpeed > 0)
            {
                moveSpeed *= decay;
                if(moveSpeed < accelaration)
                {
                    moveSpeed = 0;
                }
            }
        }

        if(targetObj != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                targetObj.GetComponent<Block>().isSelected = true;
                BlockManager.Instance.SetBlock(targetObj);
            }
        }

        animator.SetFloat("Walk", moveSpeed);
        RayHitCheack();

        //以下デバッグ用カメラの回転
        if (isRotate) { return; }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {   
            isRotate = true;
            rotateArrowAngle = 0;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            isRotate = true;
            rotateArrowAngle = 1;
        }
    }

    void FixedUpdate()
    {
        //移動関連
        PlayerMove();
        CameraRotation();
    }

    /// <summary>
    /// プレイヤーの移動
    /// </summary>
    private void PlayerMove()
    {
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1));
        Vector3 moveForward = cameraForward * v + Camera.main.transform.right * h;
        transform.position += moveForward * moveSpeed;

        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    /// <summary>
    /// 段差を登る
    /// </summary>
    private void Jump()
    {

    }

    //UI表示テスト用
    private void RayHitCheack()
    {
        Vector3 startPos =myPos + (myQ * new Vector3(0, 0.5f, 0.4f));
        Ray ray = new Ray(startPos, new Vector3(0, -1, 0));
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 0.5f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.5f))
        {
            GameObject obj = hit.collider.gameObject;
            if (obj.gameObject.layer == 10)
            {
                if (!obj.GetComponent<Block>().isTarget) { obj.GetComponent<Block>().isTarget = true; }
                if (!obj.GetComponent<Block>().isSelected)
                {
                    selectUI.sprite = images[0];
                }
                else
                {
                    selectUI.sprite = images[1];
                }

                if (targetObj != null && targetObj != obj && targetObj.GetComponent<Block>().isTarget) { targetObj.GetComponent<Block>().isTarget = false; }
                if(targetObj != obj) { targetObj = obj; }

                if (!selectUI.IsActive()) { selectUI.gameObject.SetActive(true); }
            }
            else
            {
                if (!selectUI.IsActive()) { return; }
                selectUI.gameObject.SetActive(false);
                targetObj.GetComponent<Block>().isTarget = false;
                targetObj = null;
            }
        }
        else
        {
            if (!selectUI.IsActive()) { return; }
            selectUI.gameObject.SetActive(false);
            targetObj.GetComponent<Block>().isTarget = false;
            targetObj = null;
        }
    }

    /// <summary>
    /// デバッグ用
    /// </summary>
    private void CameraRotation()
    {
        if (isRotate && sumAngle < 90)
        {
            if (rotateArrowAngle == 0)
            {
                Camera.main.transform.RotateAround(senter.position, Vector3.up, cameraAngle);
            }
            else if (rotateArrowAngle == 1)
            {
                Camera.main.transform.RotateAround(senter.position, Vector3.up, -cameraAngle);
            }

            sumAngle += cameraAngle;

            if (sumAngle >= 90)
            {
                sumAngle = 0;
                isRotate = false;
            }
        }
    }
}
