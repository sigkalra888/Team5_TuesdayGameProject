using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private RectTransform myRect;
    private Vector3 offSet = new Vector3(0, 1.5f, 0);


    void Start()
    {
        myRect = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        myRect.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position + offSet);
    }
}
