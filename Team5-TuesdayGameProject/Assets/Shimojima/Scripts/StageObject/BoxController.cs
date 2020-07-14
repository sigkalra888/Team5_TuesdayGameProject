using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] angleCollider;
    private string[] angleName = new string[4] { "left", "right", "flont", "back"};

    private struct PushFlag
    {
        public string angleName;
        public bool pushFlag;
    }

    PushFlag[] pFlag = new PushFlag[4];

    private bool isPush = false;
    
    void Start()
    {
        BoxParamInit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            PushBox(0);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            PushBox(1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            PushBox(2);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            PushBox(3);
        }

        RayCheack();
    }

    private void BoxParamInit()
    {
        for (int i = 0; i < 4; i++)
        {
            pFlag[i].angleName = angleName[i];
            pFlag[i].pushFlag = true;
        }
    }

    /// <summary>
    /// 指定方向に押す
    /// </summary>
    /// <param name="angleIndex"></param>
    public void PushBox(int angleIndex)
    {
        Debug.Log(angleIndex + ":" + pFlag[angleIndex].pushFlag);
        if (!pFlag[angleIndex].pushFlag) { Debug.Log("これ以上押せない！"); return; }

        switch (angleIndex) 
        {
            case 0:
                transform.position += new Vector3(1, 0, 0);
                break;
            case 1:
                transform.position += new Vector3(-1, 0, 0);
                break;
            case 2:
                transform.position += new Vector3(0, 0, 1);
                break;
            case 3:
                transform.position += new Vector3(0, 0, -1);
                break;
        }

    }

    /// <summary>
    /// レイを使用した周りのオブジェクトの把握処理
    /// </summary>
    private void RayCheack()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector3 rayStartPos = angleCollider[i].transform.position;
            Ray ray = new Ray();
            RaycastHit hit;
            int x = -1;
            switch (i)
            {
                case 0:
                    ray = new Ray(rayStartPos += new Vector3(0.1f, 0, 0), new Vector3(-1, 0, 0));
                    x = 1;
                    break;
                case 1:
                    ray = new Ray(rayStartPos += new Vector3(-0.1f, 0, 0), new Vector3(1, 0, 0));
                    x = 0;
                    break;
                case 2:
                    ray = new Ray(rayStartPos += new Vector3(0, 0, 0.1f), new Vector3(0, 0, -1));
                    x = 3;
                    break;
                case 3:
                    ray = new Ray(rayStartPos += new Vector3(0, 0, -0.1f), new Vector3(0, 0, 1));
                    x = 2;
                    break;
            }
            

            if (Physics.Raycast(ray, out hit, 0.5f))
            {
                //非干渉ブロックに触れている方向に対応したpushFlagをfalseにする
                if(hit.collider.gameObject.layer == 9)
                {
                    Debug.Log(hit.collider.gameObject.layer);
                    if (!pFlag[x].pushFlag) { return; }
                    Debug.Log("x:" + x);
                    pFlag[x].pushFlag = false;
                    Debug.Log(x + ":" + pFlag[x].pushFlag);
                }
            }
            
            Debug.DrawRay(ray.origin, ray.direction * 0.5f, Color.red);
        }
    }
}
