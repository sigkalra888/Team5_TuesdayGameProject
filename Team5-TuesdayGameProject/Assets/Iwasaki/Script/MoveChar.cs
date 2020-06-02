using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChar : MonoBehaviour
{
    [SerializeField]
    private float distance = 1;
    private bool hittingBool = false;
    [SerializeField]
    private GameObject BOX;
    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = BOX.GetComponent<Rigidbody>();
    }

    void Update()
    {
        CharMove();
        PushBox();
    }
    private void CharMove()
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

    private void PushBox()
    {
        if (Input.GetKeyDown(KeyCode.F) && hittingBool)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Box")
        {
            hittingBool = true;
            Debug.Log("true");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Box")
        {
            hittingBool = false;
            Debug.Log("false");
        }
    }
}
