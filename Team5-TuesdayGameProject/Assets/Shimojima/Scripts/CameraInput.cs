using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInput : MonoBehaviour
{
    [SerializeField]
    private Transform senter;

    [SerializeField]
    private float cameraAngle = 0;
    private float sumAngle = 0;
    private int rotateArrowAngle = 0;
    private bool isRotate = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

    private void FixedUpdate()
    {
        CameraRotation();
    }

    /// <summary>
    /// 俯瞰カメラ操作
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
