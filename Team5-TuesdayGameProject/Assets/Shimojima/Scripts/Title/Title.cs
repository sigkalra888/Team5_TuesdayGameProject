using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField]
    private GameObject[] animationObj;
    private Animator rogoA, fadeRogoA, flontImageA;
    private float time = 0;
    private float sTime = 0;
    private bool doOnce = false;

    void Start()
    {
        rogoA = animationObj[0].GetComponent<Animator>();
        fadeRogoA = animationObj[1].GetComponent<Animator>();
        flontImageA = animationObj[2].GetComponent<Animator>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (Input.anyKey)
        {
            PushAnyKey();
        }

        if (flontImageA.GetCurrentAnimatorStateInfo(0).IsName("Finish"))
        {
            if (!doOnce)
            {
                sTime = time;
                doOnce = true;
            }

            if (sTime + 1 < time)
            {
                SceneChanger.Instance.LoadScene(SceneChanger.SceneName.TestScene);
            }
            
        }
    }

    private void PushAnyKey()
    {
        rogoA.SetTrigger("Push");
        fadeRogoA.SetTrigger("Push");
        flontImageA.SetTrigger("Push");
    }
}
