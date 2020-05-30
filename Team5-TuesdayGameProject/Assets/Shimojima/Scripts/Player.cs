using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        //senterオブジェクトがない場合は生成して格納
        if(senter == null)
        {
            senter = new GameObject().transform;
            senter.position = new Vector3(0, 20, 0);
        }
        accelaration = 0;
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if (h  != 0 || v != 0)
        {
            //プレイヤーの加速
            if (moveSpeed < maxSpeed)
            {
                moveSpeed += accelaration;
            }
        }
        else
        {
            //プレイヤーの減速
            if (moveSpeed > 0)
            {
                moveSpeed *= decay;
            }
        }

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
    }

    /// <summary>
    /// 段差を登る
    /// </summary>
    private void Jump()
    {

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
