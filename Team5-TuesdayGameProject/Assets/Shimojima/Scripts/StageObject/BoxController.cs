using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] angleCollider;
    [SerializeField]
    private float speed;
    private string[] angleName = new string[4] { "left", "right", "flont", "back"};

    private struct PushFlag
    {
        public string angleName;
        public bool pushFlag;
    }

    PushFlag[] pFlag = new PushFlag[4];

    private int angleIndex = -1;
    private float moveDistance = 0;
    private bool isPush = false;
    private GameObject p;
    
    void Start()
    {
        BoxParamInit();
        RayCheack();
    }

    void FixedUpdate()
    {
        if (!isPush) { return; }
        PushBox();
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
    public void AngleCheackForPushBox(GameObject player)
    {
        if (isPush) { return; }
        p = player;
        angleIndex = Anglecheack(player);
        if (!pFlag[angleIndex].pushFlag || angleIndex == -1) { Debug.Log("これ以上押せない！"); return; }
        isPush = true;
    }

    private void PushBox()
    {
        switch (angleIndex)
        {
            case 0:
                transform.position += new Vector3(speed, 0, 0);
                break;
            case 1:
                transform.position += new Vector3(-speed, 0, 0);
                break;
            case 2:
                transform.position += new Vector3(0, 0, speed);
                break;
            case 3:
                transform.position += new Vector3(0, 0, -speed);
                break;
        }

        moveDistance += speed;

        if (moveDistance >= 1)
        {
            moveDistance = 0;
            isPush = false;
            p.transform.SetParent(null);
            p.GetComponent<PlayerHands>().interacting = false;
            RayCheack();
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
                if(hit.collider.gameObject.layer == 9 || hit.collider.gameObject.layer == 10 && pFlag[x].pushFlag)
                {
                    pFlag[x].pushFlag = false;
                }
            }
            else if (!pFlag[x].pushFlag)
            {
                pFlag[x].pushFlag = true;
            }
        }
    }

    private int Anglecheack(GameObject player)
    {
        Vector3 diff = player.transform.position - transform.position;
        Vector3 axis = Vector3.Cross(transform.forward, diff);
        float angle = Vector3.Angle(transform.forward, diff) * (axis.y < 0 ? -1 : 1);
        float a = angle;
        if (angle > -45 && angle <= 45)
        {
            return 3;
        }
        else if (angle > -135 && angle <= -45)
        {
            return 0;
        }
        else if (Mathf.Abs(angle) > 135 && (angle * (a < 0? 1 : -1)) <= -135)
        {
            return 2;
        }
        else if (angle > 45 && angle <= 135)
        {
            return 1;
        }
        return -1;
    }
}
