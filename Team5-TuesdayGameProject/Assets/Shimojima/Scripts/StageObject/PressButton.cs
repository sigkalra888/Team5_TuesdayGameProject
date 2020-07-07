﻿using System.Collections;
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

    private bool isPress = false;

    void Awake()
    {
        if(SceneManager.GetActiveScene().name == "StageEdit") { return; }
        animator = GetComponent<Animator>();
        defC = pressP.GetComponent<MeshRenderer>().material.color;
    }

    void Update()
    {
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
