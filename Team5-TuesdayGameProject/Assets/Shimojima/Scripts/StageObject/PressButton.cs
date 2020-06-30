using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour
{
    private Animator animator;
    private Color defC;
    [SerializeField]
    private Color pressC;

    [SerializeField]
    private GameObject pressP;

    private bool isPress = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        defC = pressP.GetComponent<MeshRenderer>().material.color;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Press();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Return();
        }
    }

    /// <summary>
    /// 押された時の処理
    /// </summary>
    public void Press()
    {
        isPress = true;
        animator.SetTrigger("Press");
        pressP.GetComponent<MeshRenderer>().material.color = pressC;
    }

    /// <summary>
    /// 押されてない状態に戻る処理
    /// </summary>
    private void Return()
    {
        isPress = false;
        animator.SetTrigger("Return");
        pressP.GetComponent<MeshRenderer>().material.color = defC;
    }
}
