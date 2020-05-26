using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChar : MonoBehaviour
{
    [SerializeField]
    private float distance = 1;

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, distance);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -distance);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-distance, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(distance, 0, 0);
        }
    }
}
