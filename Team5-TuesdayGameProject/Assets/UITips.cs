using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITips : MonoBehaviour
{

    public static Vector3 vec3, pos;
    // Use this for initialization
    void Start()
    {

        gameObject.SetActive(false);
    }

    /// <summary>
    
    /// </summary>
    public void PointerDown()
    {
        vec3 = Input.mousePosition;
        pos = transform.GetComponent<RectTransform>().position;
    }

    /// <summary>
   
    /// </summary>
    public void Drag()
    {
        Vector3 off = Input.mousePosition - vec3;
   
        vec3 = Input.mousePosition;
        pos = pos + off;
        transform.GetComponent<RectTransform>().position = pos;
    }

    /// <summary>

    /// </summary>
    public void onShow()
    {
        gameObject.SetActive(true);
    }

    /// <summary>

    /// </summary>
    public void onOK()
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

    }
}