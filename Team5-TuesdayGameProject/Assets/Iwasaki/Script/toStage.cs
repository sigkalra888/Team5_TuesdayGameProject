﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toStage : MonoBehaviour
{
    public void stage1()
    {
        SceneManager.LoadScene("Stage1");
    }
    public void stage2()
    {
        SceneManager.LoadScene("Stage2");
    }
}