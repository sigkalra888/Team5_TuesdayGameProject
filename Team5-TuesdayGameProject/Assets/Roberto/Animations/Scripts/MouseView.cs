using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseView : MonoBehaviour
{

    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes m_axes = RotationAxes.MouseXAndY;
    public float m_sensitivityX = 10f;
    public float m_sensitivityY = 10f;
    public float scaleSpeed = 5.0f;
    private float minScale = 1.0f;
    private float maxScale = 150.0f;
    private float currentScale;
    public float m_minimumX = -360f;
    public float m_maximumX = 360f;
    public float m_minimumY = -45f;
    public float m_maximumY = 45f;
    float m_rotationY = 0f;
 

    void Start()
    {
        if (Camera.main.orthographic == true)
        {
            currentScale = Camera.main.orthographicSize;
        }

        else
        {
            currentScale = Camera.main.fieldOfView;
        }

        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }


    void Update()
    {
        currentScale += Input.GetAxis("Mouse ScrollWheel") * scaleSpeed;
        currentScale = Mathf.Clamp(currentScale, minScale, maxScale);
        if (Camera.main.orthographic == true)
        {
            Camera.main.orthographicSize = currentScale;
        }

        else
        {
            Camera.main.fieldOfView = currentScale;
        }
        if (m_axes == RotationAxes.MouseXAndY)
        {
            float m_rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * m_sensitivityX;
            m_rotationY += Input.GetAxis("Mouse Y") * m_sensitivityY;
            m_rotationY = Mathf.Clamp(m_rotationY, m_minimumY, m_maximumY);
            transform.localEulerAngles = new Vector3(-m_rotationY, m_rotationX, 0);
        }
        else if (m_axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * m_sensitivityX, 0);
        }
        else
        {
            m_rotationY += Input.GetAxis("Mouse Y") * m_sensitivityY;
            m_rotationY = Mathf.Clamp(m_rotationY, m_minimumY, m_maximumY);
            transform.localEulerAngles = new Vector3(-m_rotationY, transform.localEulerAngles.y, 0);
        }
    }
}
