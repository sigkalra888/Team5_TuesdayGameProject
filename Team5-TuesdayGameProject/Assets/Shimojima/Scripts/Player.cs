using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float accelaration = 0;
    [SerializeField]
    private float maxSpeed = 0;
    private float moveSpeed = 0;

    private float h, v = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if (h  != 0 || v != 0)
        {
            if (moveSpeed < maxSpeed)
            {
                moveSpeed += accelaration;
            }
        }
        else
        {
            if (moveSpeed > 0)
            {
                moveSpeed -= accelaration * 1.2f;
            }
        }
        
    }

    void FixedUpdate()
    {
        PlayerMove();
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
}
