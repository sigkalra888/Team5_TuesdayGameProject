using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressButton : MonoBehaviour
{
    private Animator animator;
    private Color defC;
    [SerializeField]
    private Color pressC;

    [SerializeField]
    private GameObject pressP;
    public GameObject gate;

    [HideInInspector]
    public bool isPress = false;

    void Awake()
    {
        if(SceneManager.GetActiveScene().name == "StageEdit") { return; }
        animator = GetComponent<Animator>();
        defC = pressP.GetComponent<MeshRenderer>().material.color;
    }

    void Update()
    {
        RayHit();
    }

    /// <summary>
    /// 押された時の処理
    /// </summary>
    public void Press()
    {
        isPress = true;
        animator.SetTrigger("Press");
        pressP.GetComponent<MeshRenderer>().material.color = pressC;
        gate.GetComponent<Animator>().SetTrigger("Open");
    }

    /// <summary>
    /// 押されてない状態に戻る処理
    /// </summary>
    public void Return()
    {
        isPress = false;
        animator.SetTrigger("Return");
        pressP.GetComponent<MeshRenderer>().material.color = defC;
        gate.GetComponent<Animator>().SetTrigger("Close");
    }

    private void RayHit()
    {
        Vector3 myPos = pressP.transform.position + new Vector3(0, -0.3f, 0);
        Ray ray = new Ray(myPos , new Vector3(0, 1, 0));
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 0.5f);
        if (Physics.Raycast(ray, out hit, 0.5f))
        {
            Debug.Log(hit.collider.gameObject.name);
            if (isPress) { return; }
            if (hit.collider.gameObject.name == "Box")
            {
                Press();
            }
        }
        else
        {
            if (!isPress) { return; }
            Return();
        }
    }
}
